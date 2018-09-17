#include "types.hpp"
#include <map>
#include "parser.hpp"

const std::map<Type, std::string> volt::Types::volt_to_cpp = {
	{Type::auto_, "auto"},
	{Type::i8, "int8_t"},
	{Type::i16, "int16_t"},
	{Type::i32, "int32_t"},
	{Type::i64, "int64_t"},
	{Type::u8, "uint8_t"},
	{Type::u16, "uint16_t"},
	{Type::u32, "uint32_t"},
	{Type::u64, "uint64_t"},
	{Type::byte, "uint8_t"},
	{Type::usize, "size_t"},
	{Type::f32, "float"},
	{Type::f64, "double"},
	{Type::string, "std::string"},
};

const std::map<std::string, Type> volt::Types::string_to_volt = {
	{"auto", Type::auto_},
	{"i8", Type::i8},
	{"i16", Type::i16},
	{"i32", Type::i32},
	{"i64", Type::i64},
	{"u8", Type::u8},
	{"u16", Type::u16},
	{"u32", Type::u32},
	{"u64", Type::u64},
	{"byte", Type::u8},
	{"usize", Type::usize},
	{"f32", Type::f32},
	{"f64", Type::f64},
	{"string", Type::string},
};

std::string volt::Types::get_cpp_type(const Type volt_type)
{
	return volt_to_cpp.at(volt_type);
}

std::string volt::Types::get_cpp_type(const std::string& type)
{
	return get_cpp_type(get_volt_type(type));
}

Type volt::Types::get_volt_type(const std::string& type)
{
	return string_to_volt.at(type);
}
