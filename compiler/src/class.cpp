#include "class.hpp"
#include <ostream>
#include "parser.hpp"
#include <variant>
#include "types.hpp"
#include "variable.hpp"

volt::generator::Class::Class(const parser::Class& c)
	: class_(c)
{
	for (const auto& i : class_.member)
	{
		if (std::holds_alternative<parser::Variable>(i))
		{
			auto v = std::get<parser::Variable>(i);
			variables_.emplace_back(Variable(std::get<parser::Variable>(i)));
		}
	}
}

void volt::generator::Class::generate_header(std::ostream& stream)
{
	stream << "#pragma once" << std::endl;
	stream << std::endl;
	stream << "class " << class_.name << std::endl;
	stream << '{' << std::endl;
	stream << "\tprivate:" << std::endl;
	for (const auto& v : variables_)
		stream << '\t' << v.get_type_string() << ' ' << v.get_name() << ';' << std::endl;
	stream << '}' << ';' << std::endl;
	stream << std::endl;
}

void volt::generator::Class::generate_source(std::ostream& stream)
{
	stream << "#include \"" << class_.name << ".hpp\"" << std::endl;
	stream << std::endl;
	// default-constructor
	stream << class_.name << "::" << class_.name << "()" << std::endl;
	stream << "\t: ";
	for (const auto& i : variables_)
	{
		stream << i.get_name() << '{';
		switch (i.get_type())
		{
		case Type::i8:
		case Type::i16:
		case Type::i32:
		case Type::i64:
			stream << i.as_i64();
			break;
		case Type::u8:
		case Type::u16:
		case Type::u32:
		case Type::u64:
		case Type::byte:
		case Type::usize:
			stream << i.as_u64();
			break;
		case Type::f32:
		case Type::f64:
			stream << i.as_f64();
			break;
		case Type::auto_: break;
		default: break;
		}
		stream << '}';
		if (&i != &variables_.back())
			stream << ", ";
	}

	stream << std::endl;
	stream << "{}" << std::endl;
	stream << std::endl;
}

template <typename T>
std::string extract_value_from_init(const volt::parser::Variable& variable, const std::string& expected_volt_type,
                                    const std::string& expected_cpp_type)
{
	if (variable.type == expected_volt_type)
		if (std::holds_alternative<T>(variable.initialization))
			return std::to_string(std::get<T>(variable.initialization));

	throw std::runtime_error("");
}

inline void volt::generator::Class::generate_variable_definition(std::ostream& stream, const parser::Variable& v, const size_t indent)
{
	const std::string intend_s(indent, '\t');
	stream << intend_s << Types::get_cpp_type(v.type) << ' ' << v.name;
	//if (v.initialization.has_value())
	//{
	//	stream << extract_value_from_init<int8_t>(v, i8, translate_type(i8));
	//}
	stream << std::endl;
}

template <typename T>
bool check_variable(volt::parser::Variable& variable)
{
	if (!check_boundaries(variable.initialization.value_or(T(0))))
		throw std::runtime_error("value is out of bounds");
	return true;
}
