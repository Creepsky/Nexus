#pragma once

#include <iosfwd>
#include <vector>
#include "parser.hpp"
#include "variable.hpp"

namespace volt
{
	namespace parser
	{
		struct Class;
	}

	namespace generator
	{
		class Class
		{
		public:
			explicit Class(const parser::Class& c);

	        void generate_header(std::ostream& stream);
			void generate_source(std::ostream& stream);

		private:
			void generate_variable_definition(std::ostream& stream, const parser::Variable& v, size_t indent);

		private:
			const parser::Class& class_;
			std::vector<Variable> variables_;
		};
	}
}
