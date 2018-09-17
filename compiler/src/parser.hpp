#pragma once

#include <variant>
#include <string>
#include <vector>
#include <optional>

namespace volt
{
	namespace parser
	{
		using VariableDefinition = std::variant<int64_t, uint64_t, double, std::string>;

		struct Variable
		{
			std::string type;
			std::string name;
			std::optional<VariableDefinition> initialization;
		};

		using ClassDefinition = std::variant<Variable>;

		struct Class
		{
			std::string name;
			std::vector<ClassDefinition> member;
		};

		bool parse(const std::string& input, Class& ast);
	}
}
