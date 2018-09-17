#pragma once

#include "parser.hpp"
#include "types.hpp"

namespace volt
{
	namespace generator
	{
		class Variable
		{
		public:
			explicit Variable(const parser::Variable& variable);

			Type get_type() const;
			const std::string& get_type_string() const;
			const std::string& get_name() const;
			const parser::VariableDefinition& get_initialization() const;

			int8_t as_i8() const;
			int16_t as_i16() const;
			int32_t as_i32() const;
			int64_t as_i64() const;

			uint8_t as_u8() const;
			uint16_t as_u16() const;
			uint32_t as_u32() const;
			uint64_t as_u64() const;

			float as_f32() const;
			double as_f64() const;

		private:
			Type type_;
			std::string type_s_;
			std::string name_;
			parser::VariableDefinition initialization_;
		};
	}
}
