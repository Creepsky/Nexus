//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Nexus.gp4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="NexusParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface INexusVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.file_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile_declaration([NotNull] NexusParser.File_declarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] NexusParser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClass([NotNull] NexusParser.ClassContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.class_member"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClass_member([NotNull] NexusParser.Class_memberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>namedType</c>
	/// labeled alternative in <see cref="NexusParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamedType([NotNull] NexusParser.NamedTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>tupleType</c>
	/// labeled alternative in <see cref="NexusParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTupleType([NotNull] NexusParser.TupleTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>mapType</c>
	/// labeled alternative in <see cref="NexusParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMapType([NotNull] NexusParser.MapTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.variable_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable_declaration([NotNull] NexusParser.Variable_declarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.function_parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction_parameter([NotNull] NexusParser.Function_parameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.function_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction_declaration([NotNull] NexusParser.Function_declarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.assignment_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment_statement([NotNull] NexusParser.Assignment_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.return_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturn_statement([NotNull] NexusParser.Return_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.variable_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable_statement([NotNull] NexusParser.Variable_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.if_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf_statement([NotNull] NexusParser.If_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.while_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhile_statement([NotNull] NexusParser.While_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.for_init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFor_init([NotNull] NexusParser.For_initContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.for_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFor_statement([NotNull] NexusParser.For_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.function_call_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction_call_statement([NotNull] NexusParser.Function_call_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.function_body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction_body([NotNull] NexusParser.Function_bodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.function_body_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction_body_statement([NotNull] NexusParser.Function_body_statementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] NexusParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.boolean"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolean([NotNull] NexusParser.BooleanContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.array_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray_access([NotNull] NexusParser.Array_accessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] NexusParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.quoted_text"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuoted_text([NotNull] NexusParser.Quoted_textContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactor([NotNull] NexusParser.FactorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.comparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] NexusParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="NexusParser.extension_function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExtension_function([NotNull] NexusParser.Extension_functionContext context);
}