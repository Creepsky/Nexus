#include "types.hpp"
#include <map>
#include "parser.hpp"

const std::map<Type, std::string> nexus::Types::volt_to_cpp = {
	{Type::Auto, "auto"},
	{Type::I8, "int8_t"},
	{Type::I16, "int16_t"},
	{Type::I32, "int32_t"},
	{Type::I64, "int64_t"},
	{Type::U8, "uint8_t"},
	{Type::U16, "uint16_t"},
	{Type::U32, "uint32_t"},
	{Type::U64, "uint64_t"},
	{Type::Byte, "uint8_t"},
	{Type::USize, "size_t"},
	{Type::F32, "float"},
	{Type::F64, "double"},
	{Type::String, "std::string"},
};

const std::map<std::string, Type> nexus::Types::string_to_volt = {
	{"auto", Type::Auto},
	{"i8", Type::I8},
	{"i16", Type::I16},
	{"i32", Type::I32},
	{"i64", Type::I64},
	{"u8", Type::U8},
	{"u16", Type::U16},
	{"u32", Type::U32},
	{"u64", Type::U64},
	{"byte", Type::U8},
	{"usize", Type::USize},
	{"f32", Type::F32},
	{"f64", Type::F64},
	{"string", Type::String},
};

bool nexus::TypeDefinition::is_array() const
{
	return array > 0;
}

bool nexus::TypeDefinition::is_inferred() const
{
	return original_type != type;
}

bool nexus::TypeDefinition::is_tuple() const
{
	return type.front() == '(' && type.back() == ')';
}

bool nexus::TypeDefinition::is_primitive() const
{
	if (type == "string")
		return false;

	try
	{
		// primitive
		Types::get_volt_type(type);
		return true;
	}
	catch (...)
	{
		// class
		return false;
	}
}

std::string nexus::TypeDefinition::to_cpp_type() const
{
	return "";
}

std::string nexus::Types::get_cpp_type(const Type volt_type)
{
	return volt_to_cpp.at(volt_type);
}

std::string nexus::Types::get_cpp_type(const std::string& type)
{
	return get_cpp_type(get_volt_type(type));
}

Type nexus::Types::get_volt_type(const std::string& type)
{
	return string_to_volt.at(type);
}
