#define BOOST_SPIRIT_USE_PHOENIX_V3

#include <iostream>
#include <fstream>
#include <iterator>
#include "parser.hpp"

int main(const int argc, const char** argv)
{
	std::string file_path;

	if (argc > 1)
	{
		file_path = argv[1];
	}
	else
	{
		std::cerr << "Error: No input file provided." << std::endl;
		return 1;
	}

	std::ifstream file_stream(file_path);

	if (!file_stream)
	{
		std::cerr << "Error: Could not open input file: " << file_path << std::endl;
		return 1;
	}

	file_stream.unsetf(std::ios::skipws);
	
	const std::string content{
		std::istream_iterator<char>(file_stream),
		std::istream_iterator<char>(),
	};

	file_stream.close();

	nexus::parser::Class ast;

	if (parse(content, ast))
	{
		std::cout << "-------------------------" << std::endl;
		std::cout << "Parsing succeeded" << std::endl;
		std::cout << "-------------------------" << std::endl;

		//try
		//{
		//	nexus::generator::Class class_obj(ast);
		//	class_obj.generate_header(std::cout);
		//	class_obj.generate_source(std::cout);
		//}
		//catch (const std::exception& exc)
		//{
		//	std::cerr << "Error while generating C++ code: " << exc.what() << std::endl;
		//	return 1;
		//}

		//client::mini_xml_printer printer;
		//printer(ast);
		return 0;
	}

	//size_t some_length = 30;
	//const auto distance = std::distance(content.begin(), iter);
	//if (distance + some_length >= content.size())
	//	some_length = content.size() - distance;

	//const auto some = iter + some_length;
	//const std::string context(iter, some > end ? end : some);
    std::cout << "-------------------------" << std::endl;
    std::cout << "Parsing failed" << std::endl;
    //std::cout << R"(stopped at: ")" << context << R"(...")" << std::endl;
    std::cout << "-------------------------" << std::endl;
    return 1;
}
