#pragma once

#include <variant>
#include <string>
#include <vector>
#include <optional>
#include "types.hpp"

namespace nexus
{
	namespace parser
	{
		using Factor = std::variant<int64_t, uint64_t, double, std::string>;
		using Term = nullptr_t;
		using Expression = nullptr_t;
		
		struct Variable
		{
			TypeDefinition type;
			std::string name;
			bool getter, setter;
			std::optional<Expression> initialization;
		};

		struct Array
		{
			std::string type;
			std::string name;
			std::optional<std::vector<Factor>> initialization;
		};
		
		struct Function
        {
            std::string return_value;
            std::string name;
            std::vector<Variable> parameter;
        };

		using FunctionDefinition = std::variant<Variable>;

		using ClassDefinition = std::variant<Variable, Function>;

		struct Class
		{
			std::string name;
			std::vector<ClassDefinition> member;
		};

		bool parse(const std::string& input, Class& ast);
	}
}
