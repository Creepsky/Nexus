#pragma once

#include <string>
#include <map>
#include <limits>

enum class Type
{
	auto_,
    i8, i16, i32, i64,
    u8, u16, u32, u64,
    f32, f64,
    byte, usize,
	string
};

namespace volt
{
	namespace parser {
		struct Variable;
	}

	struct Types
	{
	    static const std::map<Type, std::string> volt_to_cpp;
		static const std::map<std::string, Type> string_to_volt;

		static std::string get_cpp_type(Type volt_type);
		static std::string get_cpp_type(const std::string& type);
		static Type get_volt_type(const std::string& type);
	};
}
