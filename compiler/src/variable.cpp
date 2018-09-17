#include "variable.hpp"

template <typename T>
T get_default()
{
	return T();
}

template <typename TCheck, typename T>
bool check_boundaries(T value)
{
	return value >= std::numeric_limits<TCheck>::min() && value <= std::numeric_limits<TCheck>::max();
}

template <typename TExpected, typename TCheck>
bool check_t(const volt::generator::Variable& var, volt::parser::VariableDefinition& def)
{
	if (std::holds_alternative<TCheck>(def))
	{
		if (!check_boundaries<TExpected>(std::get<TCheck>(def)))
			throw std::runtime_error("value for variable " + var.get_name() + " is out of bounds");

		return true;
	}

	return true;
}

void throw_mismatch(const volt::generator::Variable& var, volt::parser::VariableDefinition& def)
{
	std::string holds;

	if (std::holds_alternative<double>(def))
		holds = "floating point number";
	else if (std::holds_alternative<std::string>(def))
		holds = "string";
	else if (std::holds_alternative<int64_t>(def))
		holds = "signed number";
	else if (std::holds_alternative<uint64_t>(def))
		holds = "unsigned number";

	throw std::runtime_error(
		"mismatched initialization for variable " + var.get_name() + " (expected: " + var.get_type_string() +
		", got: " + holds + ")");
}

template <typename T>
void check_number(const volt::generator::Variable& var, volt::parser::VariableDefinition& def)
{
	if (!check_t<T, int64_t>(var, def) || !check_t<T, uint64_t>(var, def))
		throw_mismatch(var, def);
}

template <typename T>
void check_float(const volt::generator::Variable& var, volt::parser::VariableDefinition& def)
{
	if (!check_t<T, int64_t>(var, def) || !check_t<T, uint64_t>(var, def) || !check_t<T, double>(var, def))
		throw_mismatch(var, def);
}

void check(const volt::generator::Variable& var, volt::parser::VariableDefinition& def)
{
	switch (var.get_type())
	{
	case Type::i8: return check_number<int8_t>(var, def);
	case Type::i16: return check_number<int16_t>(var, def);
	case Type::i32: return check_number<int32_t>(var, def);
	case Type::i64: return check_number<int64_t>(var, def);
	case Type::u8: return check_number<uint8_t>(var, def);
	case Type::u16: return check_number<uint16_t>(var, def);
	case Type::u32: return check_number<uint32_t>(var, def);
	case Type::u64: return check_number<uint64_t>(var, def);
	case Type::byte: return check_number<uint8_t>(var, def);
	case Type::usize: return check_number<size_t>(var, def);
	case Type::f32: return check_float<float>(var, def);
	case Type::f64: return check_float<double>(var, def);
	case Type::string: 
		{
			if (!std::holds_alternative<std::string>(def))
				throw_mismatch(var, def);
			break;
		}
	case Type::auto_:
	default: throw std::runtime_error("could not determine auto type for variable " + var.get_name());
	}
}

volt::generator::Variable::Variable(const parser::Variable& variable)
{
	type_s_ = variable.type;
	type_ = Types::get_volt_type(type_s_);
	name_ = variable.name;

	if (variable.initialization.has_value())
	{
		initialization_ = variable.initialization.value();
	}
	else
	{
		initialization_ = [&]() -> parser::VariableDefinition {
			switch (type_)
			{
			case Type::auto_: throw std::runtime_error("could not determine auto type for variable " + name_);
			case Type::i8: return get_default<int64_t>();
			case Type::i16: return get_default<int64_t>();
			case Type::i32: return get_default<int64_t>();
			case Type::i64: return get_default<int64_t>();
			case Type::u8: return get_default<uint64_t>();
			case Type::u16: return get_default<uint64_t>();
			case Type::u32: return get_default<uint64_t>();
			case Type::u64: return get_default<uint64_t>();
			case Type::f32: return get_default<double>();
			case Type::f64: return get_default<double>();
			case Type::byte: return get_default<uint64_t>();
			case Type::usize: return get_default<size_t>();
			default: throw std::runtime_error("unknown type " + type_s_);
			}
		}();
	}

	switch (type_)
	{
	case Type::i8:
	case Type::i16: 
	case Type::i32:
	case Type::i64:
		check(*this, initialization_);
		break;
	case Type::u8:
	case Type::u16:
	case Type::u32:
	case Type::u64:
	case Type::usize:
	case Type::byte:
		check(*this, initialization_);
		break;
	case Type::f32:
	case Type::f64:
		check(*this, initialization_);
		break;
	case Type::string:
		check(*this, initialization_);
		break;
	case Type::auto_: throw std::runtime_error("not derived auto type for variable " + name_);
	default: throw std::runtime_error("unknown type " + type_s_ + " for variable " + name_);
	}
}

Type volt::generator::Variable::get_type() const
{
	return type_;
}

const std::string& volt::generator::Variable::get_type_string() const
{
	return type_s_;
}

const std::string& volt::generator::Variable::get_name() const
{
	return name_;
}

const volt::parser::VariableDefinition& volt::generator::Variable::get_initialization() const
{
	return initialization_;
}

template <typename T>
T as_t(const volt::parser::VariableDefinition& def)
{
	if (std::holds_alternative<int64_t>(def))
		return static_cast<T>(std::get<int64_t>(def));

	if (std::holds_alternative<uint64_t>(def))
		return static_cast<T>(std::get<uint64_t>(def));

	if (std::holds_alternative<double>(def))
		return static_cast<T>(std::get<double>(def));

	throw std::runtime_error("mismatched variable extraction");
}

int8_t volt::generator::Variable::as_i8() const
{
	return as_t<int8_t>(initialization_);
}

int16_t volt::generator::Variable::as_i16() const
{
	return as_t<int16_t>(initialization_);
}

int32_t volt::generator::Variable::as_i32() const
{
	return as_t<int32_t>(initialization_);
}

int64_t volt::generator::Variable::as_i64() const
{
	return as_t<int64_t>(initialization_);
}

uint8_t volt::generator::Variable::as_u8() const
{
	return as_t<uint8_t>(initialization_);
}

uint16_t volt::generator::Variable::as_u16() const
{
	return as_t<uint16_t>(initialization_);
}

uint32_t volt::generator::Variable::as_u32() const
{
	return as_t<uint32_t>(initialization_);
}

uint64_t volt::generator::Variable::as_u64() const
{
	return as_t<uint64_t>(initialization_);
}

float volt::generator::Variable::as_f32() const
{
	return as_t<float>(initialization_);
}

double volt::generator::Variable::as_f64() const
{
	return as_t<double>(initialization_);
}
