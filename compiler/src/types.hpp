#pragma once

#include <string>
#include <map>

enum class Type
{
	Auto,
    I8, I16, I32, I64,
    U8, U16, U32, U64,
    F32, F64,
    Byte, USize,
	String
};

namespace nexus
{
	struct TypeDefinition
	{
		std::string original_type;
		std::string type;
		int array;

		TypeDefinition(std::string type, const int array = 0)
			: original_type(std::move(type)), array(array)
		{
			type = original_type;
		}

		bool is_array() const;
		bool is_inferred() const;
		bool is_tuple() const;
		bool is_primitive() const;

		std::string to_cpp_type() const;
	};

	struct Types
	{
	    static const std::map<Type, std::string> volt_to_cpp;
		static const std::map<std::string, Type> string_to_volt;

		static std::string get_cpp_type(Type volt_type);
		static std::string get_cpp_type(const std::string& type);
		static Type get_volt_type(const std::string& type);
	};
}
