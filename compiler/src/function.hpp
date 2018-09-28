#pragma once

#include <string>
#include <vector>
#include <iosfwd>
#include "types.hpp"
#include "variable.hpp"

namespace volt
{
    namespace parser
    {
        struct Function;
    }

    namespace generator
    {
        class Class;

		class FunctionDefinition
		{
		public:

		private:
			std::vector<Variable> block_;
		};

        class Function
        {
        public:
            Function(const parser::Function& function, const Class* owner);

            Type get_return_type() const;
            const std::string& get_return_type_string() const;
            const std::string& get_name() const;
            const std::vector<Variable>& get_parameter() const;

            std::ostream& to_header(std::ostream& stream, size_t indent) const;
            std::ostream& to_source(std::ostream& stream, size_t indent) const;

        private:
            const Class* owner_;
            Type return_type_;
            std::string return_type_s_;
            std::string name_;
            std::vector<Variable> parameter_;
			Function _;
        };
    }
}
