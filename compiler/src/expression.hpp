#pragma once

#include <memory>
#include <string>

namespace nexus
{
	struct IExpression
	{
	public:
		virtual ~IExpression() = default;

	protected:
		IExpression() = default;
	};

	using Expression = std::unique_ptr<IExpression>;

	enum class ComparisonType
	{
		Equals,
        NotEquals,
        LessEquals,
        Less,
        GreaterEquals,
        Greater
	};

	struct Comparison : IExpression
	{
		ComparisonType type;
		Expression lhs;
		Expression rhs;

		Comparison(Expression lhs, const std::string& type, Expression rhs)
			: lhs(std::move(lhs)), rhs(std::move(rhs))
		{
			switch (type)
			{
			case "<":
				this->type = ComparisonType::Less;
				break;
			case "<":
				this->type = ComparisonType::Less;
				break;
			case "<=":
				this->type = ComparisonType::LessEquals;
				break;
			case "==":
				this->type = ComparisonType::Equals;
			break;
				case ">":
				this->type = ComparisonType::Greater;
				break;
			case ">=":
				this->type = ComparisonType::GreaterEquals;
				break;
			default:
				throw std::runtime_error("invalid comparison operator " + type);
			}
		}
	};
}
