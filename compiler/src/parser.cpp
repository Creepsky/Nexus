#include "parser.hpp"

#include <boost/spirit/include/qi.hpp>
#include <boost/spirit/include/karma.hpp>

#include <boost/variant/recursive_variant.hpp>
#include <boost/fusion/include/adapt_struct.hpp>
#include <boost/fusion/include/adapt_adt.hpp>
#include <boost/spirit/include/qi_lit.hpp>
#include <boost/spirit/include/phoenix.hpp>
#include <variant>
#include <string>
#include <optional>

namespace qi = boost::spirit::qi;
namespace ascii = boost::spirit::ascii;

BOOST_FUSION_ADAPT_STRUCT(
	nexus::TypeDefinition,
	(std::string, original_type)
)

BOOST_FUSION_ADAPT_STRUCT(
	nexus::parser::Variable,
	(nexus::TypeDefinition, type)
	(std::string, name)
	(bool, getter)
	(bool, setter)
	(std::optional<nexus::parser::Expression>, initialization)
)

BOOST_FUSION_ADAPT_STRUCT(
	nexus::parser::Class,
	(std::string, name)
	(std::vector<nexus::parser::ClassDefinition>, member)
)

BOOST_FUSION_ADAPT_STRUCT(
    nexus::parser::Function,
    (std::string, return_value)
    (std::string, name)
    (std::vector<nexus::parser::Variable>, parameter)
)

namespace nexus
{
	namespace parser
	{
		template <typename T>
		struct ClassGrammar : qi::grammar<T, Class(), ascii::space_type>
		{
			ClassGrammar()
				: ClassGrammar::base_type(class_declaration)
			{
				using boost::spirit::eps;
				using boost::spirit::lexeme;
				using boost::spirit::lit;
				using boost::spirit::short_;
				using boost::spirit::int_;
				using boost::spirit::long_;
				using boost::spirit::long_long;
				using boost::spirit::ushort_;
				using boost::spirit::uint_;
				using boost::spirit::ulong_;
				using boost::spirit::ulong_long;
				using boost::spirit::float_;
				using boost::spirit::double_;
				using boost::spirit::_1;
				using boost::spirit::_val;
				using boost::phoenix::at_c;
				using ascii::char_;

				identifier = lexeme[+char_("a-zA-Z_0-9")];
				
				quoted_text = '"' >> *(char_ - '"') >> '"';

				//factor = long_long | ulong_long | double_ | quoted_text | '[' >> *factor >> ']';

				type = identifier >> *lit("[]");
				
                variable_declaration = type >> identifier >> -('=' >> *factor);

				function_definition = variable_declaration >> ';';

			    function_declaration = identifier >> identifier >> '(' >> -(variable_declaration >> *(',' >> variable_declaration)) >> ')'
                    >> '{' >> *function_definition >> '}';

				class_definition = variable_declaration >> ';' | function_declaration;

				class_declaration = lit("class") >> identifier >> '{' >> *class_definition >> '}';

				BOOST_SPIRIT_DEBUG_NODE(identifier);
				BOOST_SPIRIT_DEBUG_NODE(variable_declaration);
				BOOST_SPIRIT_DEBUG_NODE(class_definition);
				BOOST_SPIRIT_DEBUG_NODE(class_declaration);
			}

			qi::rule<T, std::string(), ascii::space_type> identifier, quoted_text;
			qi::rule<T, Factor(), ascii::space_type> factor;
			qi::rule<T, TypeDefinition(), ascii::space_type> type;
			//qi::rule<T, Initialization(), ascii::space_type> initialization;
			qi::rule<T, Variable(), ascii::space_type> variable_declaration;
			qi::rule<T, Array(), ascii::space_type> array_declaration;
			qi::rule<T, ClassDefinition(), ascii::space_type> class_definition;
			qi::rule<T, Class(), ascii::space_type> class_declaration;
            qi::rule<T, Function(), ascii::space_type> function_declaration;
			qi::rule<T, FunctionDefinition(), ascii::space_type> function_definition;
		};
	}
}

bool nexus::parser::parse(const std::string& input, Class& ast)
{
	const ClassGrammar<std::string::const_iterator> grammar;
    auto iter = input.begin();
    const auto end = input.end();

	return phrase_parse(iter, end, grammar, ascii::space_type(), ast) && iter == end;
}
