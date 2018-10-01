// Generated from e:\dev\Volt\NexusParserAntlr\NexusParserAntlr\Nexus.gp4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class NexusParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		CLASS=1, LEFT_CURLY_BRACE=2, RIGHT_CURLY_BRACE=3, LEFT_BRACE=4, RIGHT_BRACE=5, 
		ARRAY_DECLARATION=6, LEFT_BRACKET=7, RIGHT_BRACKET=8, COLON=9, ARROW_RIGHT=10, 
		BINARY=11, HEX=12, INTEGER=13, REAL=14, IF=15, ELSE=16, ELSE_IF=17, WHILE=18, 
		FOR=19, APOSTROPHE=20, EQUAL=21, LESS=22, LESS_EQUAL=23, GREATER=24, GREATER_EQUAL=25, 
		TRUE=26, FALSE=27, PLUS=28, MINUS=29, STAR=30, SLASH=31, SET=32, GET=33, 
		COMMA=34, RETURN=35, RANGE=36, DOT=37, INTEGER_SUFFIX=38, REAL_SUFFIX=39, 
		IDENTIFIER=40, WHITESPACE=41;
	public static final int
		RULE_file_declaration = 0, RULE_file = 1, RULE_class = 2, RULE_class_member = 3, 
		RULE_type = 4, RULE_variable_declaration = 5, RULE_function_parameter = 6, 
		RULE_function_declaration = 7, RULE_assignment_statement = 8, RULE_return_statement = 9, 
		RULE_variable_statement = 10, RULE_if_statement = 11, RULE_while_statement = 12, 
		RULE_for_init = 13, RULE_for_statement = 14, RULE_function_call_statement = 15, 
		RULE_function_body = 16, RULE_function_body_statement = 17, RULE_expression = 18, 
		RULE_boolean = 19, RULE_array_access = 20, RULE_number = 21, RULE_quoted_text = 22, 
		RULE_factor = 23, RULE_comparison = 24, RULE_extension_function = 25;
	public static final String[] ruleNames = {
		"file_declaration", "file", "class", "class_member", "type", "variable_declaration", 
		"function_parameter", "function_declaration", "assignment_statement", 
		"return_statement", "variable_statement", "if_statement", "while_statement", 
		"for_init", "for_statement", "function_call_statement", "function_body", 
		"function_body_statement", "expression", "boolean", "array_access", "number", 
		"quoted_text", "factor", "comparison", "extension_function"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'class'", "'{'", "'}'", "'('", "')'", "'[]'", "'['", "']'", "';'", 
		"'->'", null, null, null, null, "'if'", "'else'", null, "'while'", "'for'", 
		"'\"'", "'='", "'<'", "'<='", "'>'", "'>='", "'true'", "'false'", "'+'", 
		"'-'", "'*'", "'/'", "'set'", "'get'", "','", "'return'", "'..'", "'.'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "CLASS", "LEFT_CURLY_BRACE", "RIGHT_CURLY_BRACE", "LEFT_BRACE", 
		"RIGHT_BRACE", "ARRAY_DECLARATION", "LEFT_BRACKET", "RIGHT_BRACKET", "COLON", 
		"ARROW_RIGHT", "BINARY", "HEX", "INTEGER", "REAL", "IF", "ELSE", "ELSE_IF", 
		"WHILE", "FOR", "APOSTROPHE", "EQUAL", "LESS", "LESS_EQUAL", "GREATER", 
		"GREATER_EQUAL", "TRUE", "FALSE", "PLUS", "MINUS", "STAR", "SLASH", "SET", 
		"GET", "COMMA", "RETURN", "RANGE", "DOT", "INTEGER_SUFFIX", "REAL_SUFFIX", 
		"IDENTIFIER", "WHITESPACE"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Nexus.gp4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public NexusParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class File_declarationContext extends ParserRuleContext {
		public ClassContext class() {
			return getRuleContext(ClassContext.class,0);
		}
		public Extension_functionContext extension_function() {
			return getRuleContext(Extension_functionContext.class,0);
		}
		public File_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_file_declaration; }
	}

	public final File_declarationContext file_declaration() throws RecognitionException {
		File_declarationContext _localctx = new File_declarationContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_file_declaration);
		try {
			setState(54);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case CLASS:
				enterOuterAlt(_localctx, 1);
				{
				setState(52);
				class();
				}
				break;
			case LEFT_BRACE:
			case LEFT_BRACKET:
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(53);
				extension_function();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FileContext extends ParserRuleContext {
		public List<File_declarationContext> file_declaration() {
			return getRuleContexts(File_declarationContext.class);
		}
		public File_declarationContext file_declaration(int i) {
			return getRuleContext(File_declarationContext.class,i);
		}
		public FileContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_file; }
	}

	public final FileContext file() throws RecognitionException {
		FileContext _localctx = new FileContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_file);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << CLASS) | (1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(56);
				file_declaration();
				}
				}
				setState(61);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ClassContext extends ParserRuleContext {
		public Token name;
		public TerminalNode CLASS() { return getToken(NexusParser.CLASS, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public List<Class_memberContext> class_member() {
			return getRuleContexts(Class_memberContext.class);
		}
		public Class_memberContext class_member(int i) {
			return getRuleContext(Class_memberContext.class,i);
		}
		public ClassContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_class; }
	}

	public final ClassContext class() throws RecognitionException {
		ClassContext _localctx = new ClassContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_class);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(62);
			match(CLASS);
			setState(63);
			((ClassContext)_localctx).name = match(IDENTIFIER);
			setState(64);
			match(LEFT_CURLY_BRACE);
			setState(68);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(65);
				class_member();
				}
				}
				setState(70);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(71);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Class_memberContext extends ParserRuleContext {
		public Variable_declarationContext variable_declaration() {
			return getRuleContext(Variable_declarationContext.class,0);
		}
		public Function_declarationContext function_declaration() {
			return getRuleContext(Function_declarationContext.class,0);
		}
		public Class_memberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_class_member; }
	}

	public final Class_memberContext class_member() throws RecognitionException {
		Class_memberContext _localctx = new Class_memberContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_class_member);
		try {
			setState(75);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(73);
				variable_declaration();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(74);
				function_declaration();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	 
		public TypeContext() { }
		public void copyFrom(TypeContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class NamedTypeContext extends TypeContext {
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public List<TerminalNode> ARRAY_DECLARATION() { return getTokens(NexusParser.ARRAY_DECLARATION); }
		public TerminalNode ARRAY_DECLARATION(int i) {
			return getToken(NexusParser.ARRAY_DECLARATION, i);
		}
		public NamedTypeContext(TypeContext ctx) { copyFrom(ctx); }
	}
	public static class MapTypeContext extends TypeContext {
		public TypeContext key;
		public TypeContext value;
		public TerminalNode LEFT_BRACKET() { return getToken(NexusParser.LEFT_BRACKET, 0); }
		public TerminalNode ARROW_RIGHT() { return getToken(NexusParser.ARROW_RIGHT, 0); }
		public TerminalNode RIGHT_BRACKET() { return getToken(NexusParser.RIGHT_BRACKET, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public List<TerminalNode> ARRAY_DECLARATION() { return getTokens(NexusParser.ARRAY_DECLARATION); }
		public TerminalNode ARRAY_DECLARATION(int i) {
			return getToken(NexusParser.ARRAY_DECLARATION, i);
		}
		public MapTypeContext(TypeContext ctx) { copyFrom(ctx); }
	}
	public static class TupleTypeContext extends TypeContext {
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public List<TerminalNode> ARRAY_DECLARATION() { return getTokens(NexusParser.ARRAY_DECLARATION); }
		public TerminalNode ARRAY_DECLARATION(int i) {
			return getToken(NexusParser.ARRAY_DECLARATION, i);
		}
		public TupleTypeContext(TypeContext ctx) { copyFrom(ctx); }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_type);
		int _la;
		try {
			setState(111);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IDENTIFIER:
				_localctx = new NamedTypeContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(77);
				match(IDENTIFIER);
				setState(81);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==ARRAY_DECLARATION) {
					{
					{
					setState(78);
					match(ARRAY_DECLARATION);
					}
					}
					setState(83);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case LEFT_BRACE:
				_localctx = new TupleTypeContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(84);
				match(LEFT_BRACE);
				setState(85);
				type();
				setState(90);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(86);
					match(COMMA);
					setState(87);
					type();
					}
					}
					setState(92);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(93);
				match(RIGHT_BRACE);
				setState(97);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==ARRAY_DECLARATION) {
					{
					{
					setState(94);
					match(ARRAY_DECLARATION);
					}
					}
					setState(99);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case LEFT_BRACKET:
				_localctx = new MapTypeContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(100);
				match(LEFT_BRACKET);
				setState(101);
				((MapTypeContext)_localctx).key = type();
				setState(102);
				match(ARROW_RIGHT);
				setState(103);
				((MapTypeContext)_localctx).value = type();
				setState(104);
				match(RIGHT_BRACKET);
				setState(108);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==ARRAY_DECLARATION) {
					{
					{
					setState(105);
					match(ARRAY_DECLARATION);
					}
					}
					setState(110);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Variable_declarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public TerminalNode SET() { return getToken(NexusParser.SET, 0); }
		public TerminalNode GET() { return getToken(NexusParser.GET, 0); }
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Variable_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variable_declaration; }
	}

	public final Variable_declarationContext variable_declaration() throws RecognitionException {
		Variable_declarationContext _localctx = new Variable_declarationContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_variable_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(113);
			type();
			setState(114);
			match(IDENTIFIER);
			setState(116);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==SET) {
				{
				setState(115);
				match(SET);
				}
			}

			setState(119);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==GET) {
				{
				setState(118);
				match(GET);
				}
			}

			setState(123);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(121);
				match(EQUAL);
				setState(122);
				expression(0);
				}
			}

			setState(125);
			match(COLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_parameterContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Function_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_parameter; }
	}

	public final Function_parameterContext function_parameter() throws RecognitionException {
		Function_parameterContext _localctx = new Function_parameterContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_function_parameter);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(127);
			type();
			setState(128);
			match(IDENTIFIER);
			setState(131);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(129);
				match(EQUAL);
				setState(130);
				expression(0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_declarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public Function_bodyContext function_body() {
			return getRuleContext(Function_bodyContext.class,0);
		}
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<Function_parameterContext> function_parameter() {
			return getRuleContexts(Function_parameterContext.class);
		}
		public Function_parameterContext function_parameter(int i) {
			return getRuleContext(Function_parameterContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public Function_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_declaration; }
	}

	public final Function_declarationContext function_declaration() throws RecognitionException {
		Function_declarationContext _localctx = new Function_declarationContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_function_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			type();
			setState(134);
			match(IDENTIFIER);
			setState(135);
			match(LEFT_BRACE);
			setState(144);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(136);
				function_parameter();
				setState(141);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(137);
					match(COMMA);
					setState(138);
					function_parameter();
					}
					}
					setState(143);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(146);
			match(RIGHT_BRACE);
			setState(147);
			match(LEFT_CURLY_BRACE);
			setState(148);
			function_body();
			setState(149);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Assignment_statementContext extends ParserRuleContext {
		public ExpressionContext left;
		public ExpressionContext right;
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public Assignment_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment_statement; }
	}

	public final Assignment_statementContext assignment_statement() throws RecognitionException {
		Assignment_statementContext _localctx = new Assignment_statementContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_assignment_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(151);
			((Assignment_statementContext)_localctx).left = expression(0);
			setState(152);
			match(EQUAL);
			setState(153);
			((Assignment_statementContext)_localctx).right = expression(0);
			setState(154);
			match(COLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Return_statementContext extends ParserRuleContext {
		public TerminalNode RETURN() { return getToken(NexusParser.RETURN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public Return_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_return_statement; }
	}

	public final Return_statementContext return_statement() throws RecognitionException {
		Return_statementContext _localctx = new Return_statementContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_return_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(156);
			match(RETURN);
			setState(157);
			expression(0);
			setState(158);
			match(COLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Variable_statementContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Variable_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variable_statement; }
	}

	public final Variable_statementContext variable_statement() throws RecognitionException {
		Variable_statementContext _localctx = new Variable_statementContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_variable_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(160);
			type();
			setState(161);
			match(IDENTIFIER);
			setState(164);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(162);
				match(EQUAL);
				setState(163);
				expression(0);
				}
			}

			setState(166);
			match(COLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class If_statementContext extends ParserRuleContext {
		public Function_bodyContext then;
		public Function_bodyContext else;
		public TerminalNode IF() { return getToken(NexusParser.IF, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public ComparisonContext comparison() {
			return getRuleContext(ComparisonContext.class,0);
		}
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public List<TerminalNode> LEFT_CURLY_BRACE() { return getTokens(NexusParser.LEFT_CURLY_BRACE); }
		public TerminalNode LEFT_CURLY_BRACE(int i) {
			return getToken(NexusParser.LEFT_CURLY_BRACE, i);
		}
		public List<TerminalNode> RIGHT_CURLY_BRACE() { return getTokens(NexusParser.RIGHT_CURLY_BRACE); }
		public TerminalNode RIGHT_CURLY_BRACE(int i) {
			return getToken(NexusParser.RIGHT_CURLY_BRACE, i);
		}
		public TerminalNode ELSE() { return getToken(NexusParser.ELSE, 0); }
		public List<Function_bodyContext> function_body() {
			return getRuleContexts(Function_bodyContext.class);
		}
		public Function_bodyContext function_body(int i) {
			return getRuleContext(Function_bodyContext.class,i);
		}
		public If_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_if_statement; }
	}

	public final If_statementContext if_statement() throws RecognitionException {
		If_statementContext _localctx = new If_statementContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_if_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(168);
			match(IF);
			setState(169);
			match(LEFT_BRACE);
			setState(170);
			comparison();
			setState(171);
			match(RIGHT_BRACE);
			setState(172);
			match(LEFT_CURLY_BRACE);
			setState(173);
			((If_statementContext)_localctx).then = function_body();
			setState(174);
			match(RIGHT_CURLY_BRACE);
			setState(175);
			match(ELSE);
			setState(176);
			match(LEFT_CURLY_BRACE);
			setState(177);
			((If_statementContext)_localctx).else = function_body();
			setState(178);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class While_statementContext extends ParserRuleContext {
		public TerminalNode WHILE() { return getToken(NexusParser.WHILE, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public ComparisonContext comparison() {
			return getRuleContext(ComparisonContext.class,0);
		}
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public Function_bodyContext function_body() {
			return getRuleContext(Function_bodyContext.class,0);
		}
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public While_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_while_statement; }
	}

	public final While_statementContext while_statement() throws RecognitionException {
		While_statementContext _localctx = new While_statementContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_while_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(180);
			match(WHILE);
			setState(181);
			match(LEFT_BRACE);
			setState(182);
			comparison();
			setState(183);
			match(RIGHT_BRACE);
			setState(184);
			match(LEFT_CURLY_BRACE);
			setState(185);
			function_body();
			setState(186);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class For_initContext extends ParserRuleContext {
		public Assignment_statementContext assignment_statement() {
			return getRuleContext(Assignment_statementContext.class,0);
		}
		public Variable_statementContext variable_statement() {
			return getRuleContext(Variable_statementContext.class,0);
		}
		public For_initContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_for_init; }
	}

	public final For_initContext for_init() throws RecognitionException {
		For_initContext _localctx = new For_initContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_for_init);
		try {
			setState(190);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(188);
				assignment_statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(189);
				variable_statement();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class For_statementContext extends ParserRuleContext {
		public TerminalNode FOR() { return getToken(NexusParser.FOR, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public For_initContext for_init() {
			return getRuleContext(For_initContext.class,0);
		}
		public ComparisonContext comparison() {
			return getRuleContext(ComparisonContext.class,0);
		}
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public Function_bodyContext function_body() {
			return getRuleContext(Function_bodyContext.class,0);
		}
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public For_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_for_statement; }
	}

	public final For_statementContext for_statement() throws RecognitionException {
		For_statementContext _localctx = new For_statementContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_for_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(192);
			match(FOR);
			setState(193);
			match(LEFT_BRACE);
			setState(194);
			for_init();
			setState(195);
			comparison();
			setState(196);
			match(COLON);
			setState(197);
			expression(0);
			setState(198);
			match(RIGHT_BRACE);
			setState(199);
			match(LEFT_CURLY_BRACE);
			setState(200);
			function_body();
			setState(201);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_call_statementContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public Function_call_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_call_statement; }
	}

	public final Function_call_statementContext function_call_statement() throws RecognitionException {
		Function_call_statementContext _localctx = new Function_call_statementContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_function_call_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(203);
			match(IDENTIFIER);
			setState(204);
			match(LEFT_BRACE);
			setState(213);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << APOSTROPHE) | (1L << TRUE) | (1L << FALSE) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(205);
				expression(0);
				setState(210);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(206);
					match(COMMA);
					setState(207);
					expression(0);
					}
					}
					setState(212);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(215);
			match(RIGHT_BRACE);
			setState(216);
			match(COLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_bodyContext extends ParserRuleContext {
		public List<Function_body_statementContext> function_body_statement() {
			return getRuleContexts(Function_body_statementContext.class);
		}
		public Function_body_statementContext function_body_statement(int i) {
			return getRuleContext(Function_body_statementContext.class,i);
		}
		public Function_bodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body; }
	}

	public final Function_bodyContext function_body() throws RecognitionException {
		Function_bodyContext _localctx = new Function_bodyContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_function_body);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(221);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << APOSTROPHE) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(218);
				function_body_statement();
				}
				}
				setState(223);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_body_statementContext extends ParserRuleContext {
		public Assignment_statementContext assignment_statement() {
			return getRuleContext(Assignment_statementContext.class,0);
		}
		public Return_statementContext return_statement() {
			return getRuleContext(Return_statementContext.class,0);
		}
		public Function_declarationContext function_declaration() {
			return getRuleContext(Function_declarationContext.class,0);
		}
		public Variable_statementContext variable_statement() {
			return getRuleContext(Variable_statementContext.class,0);
		}
		public If_statementContext if_statement() {
			return getRuleContext(If_statementContext.class,0);
		}
		public While_statementContext while_statement() {
			return getRuleContext(While_statementContext.class,0);
		}
		public For_statementContext for_statement() {
			return getRuleContext(For_statementContext.class,0);
		}
		public Function_call_statementContext function_call_statement() {
			return getRuleContext(Function_call_statementContext.class,0);
		}
		public Function_body_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body_statement; }
	}

	public final Function_body_statementContext function_body_statement() throws RecognitionException {
		Function_body_statementContext _localctx = new Function_body_statementContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_function_body_statement);
		try {
			setState(232);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(224);
				assignment_statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(225);
				return_statement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(226);
				function_declaration();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(227);
				variable_statement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(228);
				if_statement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(229);
				while_statement();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(230);
				for_statement();
				}
				break;
			case 8:
				enterOuterAlt(_localctx, 8);
				{
				setState(231);
				function_call_statement();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public TerminalNode LEFT_BRACKET() { return getToken(NexusParser.LEFT_BRACKET, 0); }
		public TerminalNode RIGHT_BRACKET() { return getToken(NexusParser.RIGHT_BRACKET, 0); }
		public List<TerminalNode> ARROW_RIGHT() { return getTokens(NexusParser.ARROW_RIGHT); }
		public TerminalNode ARROW_RIGHT(int i) {
			return getToken(NexusParser.ARROW_RIGHT, i);
		}
		public FactorContext factor() {
			return getRuleContext(FactorContext.class,0);
		}
		public TerminalNode STAR() { return getToken(NexusParser.STAR, 0); }
		public TerminalNode SLASH() { return getToken(NexusParser.SLASH, 0); }
		public TerminalNode PLUS() { return getToken(NexusParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(NexusParser.MINUS, 0); }
		public TerminalNode RANGE() { return getToken(NexusParser.RANGE, 0); }
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 36;
		enterRecursionRule(_localctx, 36, RULE_expression, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(277);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,24,_ctx) ) {
			case 1:
				{
				setState(235);
				match(LEFT_BRACE);
				setState(236);
				expression(0);
				setState(239); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(237);
					match(COMMA);
					setState(238);
					expression(0);
					}
					}
					setState(241); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==COMMA );
				setState(243);
				match(RIGHT_BRACE);
				}
				break;
			case 2:
				{
				setState(245);
				match(LEFT_BRACE);
				setState(246);
				expression(0);
				setState(247);
				match(RIGHT_BRACE);
				}
				break;
			case 3:
				{
				setState(249);
				match(LEFT_BRACKET);
				setState(250);
				expression(0);
				setState(255);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(251);
					match(COMMA);
					setState(252);
					expression(0);
					}
					}
					setState(257);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(258);
				match(RIGHT_BRACKET);
				}
				break;
			case 4:
				{
				setState(260);
				match(LEFT_BRACKET);
				setState(261);
				expression(0);
				setState(262);
				match(ARROW_RIGHT);
				setState(263);
				expression(0);
				setState(271);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(264);
					match(COMMA);
					setState(265);
					expression(0);
					setState(266);
					match(ARROW_RIGHT);
					setState(267);
					expression(0);
					}
					}
					setState(273);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(274);
				match(RIGHT_BRACKET);
				}
				break;
			case 5:
				{
				setState(276);
				factor();
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(299);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,26,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(297);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(279);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(280);
						match(STAR);
						setState(281);
						expression(12);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(282);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(283);
						match(SLASH);
						setState(284);
						expression(11);
						}
						break;
					case 3:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(285);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(286);
						match(PLUS);
						setState(287);
						expression(10);
						}
						break;
					case 4:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(288);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(289);
						match(MINUS);
						setState(290);
						expression(9);
						}
						break;
					case 5:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(291);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(292);
						match(RANGE);
						setState(293);
						expression(4);
						}
						break;
					case 6:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(294);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(295);
						match(EQUAL);
						setState(296);
						expression(3);
						}
						break;
					}
					} 
				}
				setState(301);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,26,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class BooleanContext extends ParserRuleContext {
		public TerminalNode TRUE() { return getToken(NexusParser.TRUE, 0); }
		public TerminalNode FALSE() { return getToken(NexusParser.FALSE, 0); }
		public BooleanContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_boolean; }
	}

	public final BooleanContext boolean() throws RecognitionException {
		BooleanContext _localctx = new BooleanContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_boolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(302);
			_la = _input.LA(1);
			if ( !(_la==TRUE || _la==FALSE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Array_accessContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode LEFT_BRACKET() { return getToken(NexusParser.LEFT_BRACKET, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RIGHT_BRACKET() { return getToken(NexusParser.RIGHT_BRACKET, 0); }
		public Array_accessContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_access; }
	}

	public final Array_accessContext array_access() throws RecognitionException {
		Array_accessContext _localctx = new Array_accessContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_array_access);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(304);
			match(IDENTIFIER);
			setState(305);
			match(LEFT_BRACKET);
			setState(306);
			expression(0);
			setState(307);
			match(RIGHT_BRACKET);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumberContext extends ParserRuleContext {
		public TerminalNode INTEGER() { return getToken(NexusParser.INTEGER, 0); }
		public TerminalNode INTEGER_SUFFIX() { return getToken(NexusParser.INTEGER_SUFFIX, 0); }
		public TerminalNode REAL() { return getToken(NexusParser.REAL, 0); }
		public TerminalNode REAL_SUFFIX() { return getToken(NexusParser.REAL_SUFFIX, 0); }
		public NumberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_number; }
	}

	public final NumberContext number() throws RecognitionException {
		NumberContext _localctx = new NumberContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_number);
		try {
			setState(317);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INTEGER:
				enterOuterAlt(_localctx, 1);
				{
				setState(309);
				match(INTEGER);
				setState(311);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
				case 1:
					{
					setState(310);
					match(INTEGER_SUFFIX);
					}
					break;
				}
				}
				break;
			case REAL:
				enterOuterAlt(_localctx, 2);
				{
				setState(313);
				match(REAL);
				setState(315);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,28,_ctx) ) {
				case 1:
					{
					setState(314);
					match(REAL_SUFFIX);
					}
					break;
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Quoted_textContext extends ParserRuleContext {
		public Token text;
		public List<TerminalNode> APOSTROPHE() { return getTokens(NexusParser.APOSTROPHE); }
		public TerminalNode APOSTROPHE(int i) {
			return getToken(NexusParser.APOSTROPHE, i);
		}
		public Quoted_textContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_quoted_text; }
	}

	public final Quoted_textContext quoted_text() throws RecognitionException {
		Quoted_textContext _localctx = new Quoted_textContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_quoted_text);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(319);
			match(APOSTROPHE);
			setState(323);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,30,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(320);
					((Quoted_textContext)_localctx).text = matchWildcard();
					}
					} 
				}
				setState(325);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,30,_ctx);
			}
			setState(326);
			match(APOSTROPHE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FactorContext extends ParserRuleContext {
		public NumberContext number() {
			return getRuleContext(NumberContext.class,0);
		}
		public BooleanContext boolean() {
			return getRuleContext(BooleanContext.class,0);
		}
		public TerminalNode BINARY() { return getToken(NexusParser.BINARY, 0); }
		public TerminalNode HEX() { return getToken(NexusParser.HEX, 0); }
		public Quoted_textContext quoted_text() {
			return getRuleContext(Quoted_textContext.class,0);
		}
		public Array_accessContext array_access() {
			return getRuleContext(Array_accessContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public FactorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_factor; }
	}

	public final FactorContext factor() throws RecognitionException {
		FactorContext _localctx = new FactorContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_factor);
		try {
			setState(335);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,31,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(328);
				number();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(329);
				boolean();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(330);
				match(BINARY);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(331);
				match(HEX);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(332);
				quoted_text();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(333);
				array_access();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(334);
				match(IDENTIFIER);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComparisonContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public TerminalNode LESS() { return getToken(NexusParser.LESS, 0); }
		public TerminalNode LESS_EQUAL() { return getToken(NexusParser.LESS_EQUAL, 0); }
		public TerminalNode GREATER() { return getToken(NexusParser.GREATER, 0); }
		public TerminalNode GREATER_EQUAL() { return getToken(NexusParser.GREATER_EQUAL, 0); }
		public ComparisonContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_comparison; }
	}

	public final ComparisonContext comparison() throws RecognitionException {
		ComparisonContext _localctx = new ComparisonContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_comparison);
		try {
			setState(357);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(337);
				expression(0);
				setState(338);
				match(EQUAL);
				setState(339);
				expression(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(341);
				expression(0);
				setState(342);
				match(LESS);
				setState(343);
				expression(0);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(345);
				expression(0);
				setState(346);
				match(LESS_EQUAL);
				setState(347);
				expression(0);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(349);
				expression(0);
				setState(350);
				match(GREATER);
				setState(351);
				expression(0);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(353);
				expression(0);
				setState(354);
				match(GREATER_EQUAL);
				setState(355);
				expression(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Extension_functionContext extends ParserRuleContext {
		public Token className;
		public Token name;
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode DOT() { return getToken(NexusParser.DOT, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public Function_bodyContext function_body() {
			return getRuleContext(Function_bodyContext.class,0);
		}
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<TerminalNode> IDENTIFIER() { return getTokens(NexusParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(NexusParser.IDENTIFIER, i);
		}
		public List<Function_parameterContext> function_parameter() {
			return getRuleContexts(Function_parameterContext.class);
		}
		public Function_parameterContext function_parameter(int i) {
			return getRuleContext(Function_parameterContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public Extension_functionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_extension_function; }
	}

	public final Extension_functionContext extension_function() throws RecognitionException {
		Extension_functionContext _localctx = new Extension_functionContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_extension_function);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(359);
			type();
			setState(360);
			((Extension_functionContext)_localctx).className = match(IDENTIFIER);
			setState(361);
			match(DOT);
			setState(362);
			((Extension_functionContext)_localctx).name = match(IDENTIFIER);
			setState(363);
			match(LEFT_BRACE);
			setState(372);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(364);
				function_parameter();
				setState(369);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(365);
					match(COMMA);
					setState(366);
					function_parameter();
					}
					}
					setState(371);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(374);
			match(RIGHT_BRACE);
			setState(375);
			match(LEFT_CURLY_BRACE);
			setState(376);
			function_body();
			setState(377);
			match(RIGHT_CURLY_BRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 18:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 11);
		case 1:
			return precpred(_ctx, 10);
		case 2:
			return precpred(_ctx, 9);
		case 3:
			return precpred(_ctx, 8);
		case 4:
			return precpred(_ctx, 3);
		case 5:
			return precpred(_ctx, 2);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3+\u017e\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\3\2\3\2\5\29\n\2\3\3\7\3<\n\3\f\3\16\3?\13\3\3\4"+
		"\3\4\3\4\3\4\7\4E\n\4\f\4\16\4H\13\4\3\4\3\4\3\5\3\5\5\5N\n\5\3\6\3\6"+
		"\7\6R\n\6\f\6\16\6U\13\6\3\6\3\6\3\6\3\6\7\6[\n\6\f\6\16\6^\13\6\3\6\3"+
		"\6\7\6b\n\6\f\6\16\6e\13\6\3\6\3\6\3\6\3\6\3\6\3\6\7\6m\n\6\f\6\16\6p"+
		"\13\6\5\6r\n\6\3\7\3\7\3\7\5\7w\n\7\3\7\5\7z\n\7\3\7\3\7\5\7~\n\7\3\7"+
		"\3\7\3\b\3\b\3\b\3\b\5\b\u0086\n\b\3\t\3\t\3\t\3\t\3\t\3\t\7\t\u008e\n"+
		"\t\f\t\16\t\u0091\13\t\5\t\u0093\n\t\3\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3"+
		"\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\5\f\u00a7\n\f\3\f\3\f\3\r\3"+
		"\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\16\3"+
		"\16\3\16\3\16\3\17\3\17\5\17\u00c1\n\17\3\20\3\20\3\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\21\3\21\3\21\7\21\u00d3\n\21\f\21"+
		"\16\21\u00d6\13\21\5\21\u00d8\n\21\3\21\3\21\3\21\3\22\7\22\u00de\n\22"+
		"\f\22\16\22\u00e1\13\22\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\5\23\u00eb"+
		"\n\23\3\24\3\24\3\24\3\24\3\24\6\24\u00f2\n\24\r\24\16\24\u00f3\3\24\3"+
		"\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\7\24\u0100\n\24\f\24\16\24"+
		"\u0103\13\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\7"+
		"\24\u0110\n\24\f\24\16\24\u0113\13\24\3\24\3\24\3\24\5\24\u0118\n\24\3"+
		"\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3"+
		"\24\3\24\3\24\3\24\7\24\u012c\n\24\f\24\16\24\u012f\13\24\3\25\3\25\3"+
		"\26\3\26\3\26\3\26\3\26\3\27\3\27\5\27\u013a\n\27\3\27\3\27\5\27\u013e"+
		"\n\27\5\27\u0140\n\27\3\30\3\30\7\30\u0144\n\30\f\30\16\30\u0147\13\30"+
		"\3\30\3\30\3\31\3\31\3\31\3\31\3\31\3\31\3\31\5\31\u0152\n\31\3\32\3\32"+
		"\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32"+
		"\3\32\3\32\3\32\3\32\5\32\u0168\n\32\3\33\3\33\3\33\3\33\3\33\3\33\3\33"+
		"\3\33\7\33\u0172\n\33\f\33\16\33\u0175\13\33\5\33\u0177\n\33\3\33\3\33"+
		"\3\33\3\33\3\33\3\33\3\u0145\3&\34\2\4\6\b\n\f\16\20\22\24\26\30\32\34"+
		"\36 \"$&(*,.\60\62\64\2\3\3\2\34\35\2\u019c\28\3\2\2\2\4=\3\2\2\2\6@\3"+
		"\2\2\2\bM\3\2\2\2\nq\3\2\2\2\fs\3\2\2\2\16\u0081\3\2\2\2\20\u0087\3\2"+
		"\2\2\22\u0099\3\2\2\2\24\u009e\3\2\2\2\26\u00a2\3\2\2\2\30\u00aa\3\2\2"+
		"\2\32\u00b6\3\2\2\2\34\u00c0\3\2\2\2\36\u00c2\3\2\2\2 \u00cd\3\2\2\2\""+
		"\u00df\3\2\2\2$\u00ea\3\2\2\2&\u0117\3\2\2\2(\u0130\3\2\2\2*\u0132\3\2"+
		"\2\2,\u013f\3\2\2\2.\u0141\3\2\2\2\60\u0151\3\2\2\2\62\u0167\3\2\2\2\64"+
		"\u0169\3\2\2\2\669\5\6\4\2\679\5\64\33\28\66\3\2\2\28\67\3\2\2\29\3\3"+
		"\2\2\2:<\5\2\2\2;:\3\2\2\2<?\3\2\2\2=;\3\2\2\2=>\3\2\2\2>\5\3\2\2\2?="+
		"\3\2\2\2@A\7\3\2\2AB\7*\2\2BF\7\4\2\2CE\5\b\5\2DC\3\2\2\2EH\3\2\2\2FD"+
		"\3\2\2\2FG\3\2\2\2GI\3\2\2\2HF\3\2\2\2IJ\7\5\2\2J\7\3\2\2\2KN\5\f\7\2"+
		"LN\5\20\t\2MK\3\2\2\2ML\3\2\2\2N\t\3\2\2\2OS\7*\2\2PR\7\b\2\2QP\3\2\2"+
		"\2RU\3\2\2\2SQ\3\2\2\2ST\3\2\2\2Tr\3\2\2\2US\3\2\2\2VW\7\6\2\2W\\\5\n"+
		"\6\2XY\7$\2\2Y[\5\n\6\2ZX\3\2\2\2[^\3\2\2\2\\Z\3\2\2\2\\]\3\2\2\2]_\3"+
		"\2\2\2^\\\3\2\2\2_c\7\7\2\2`b\7\b\2\2a`\3\2\2\2be\3\2\2\2ca\3\2\2\2cd"+
		"\3\2\2\2dr\3\2\2\2ec\3\2\2\2fg\7\t\2\2gh\5\n\6\2hi\7\f\2\2ij\5\n\6\2j"+
		"n\7\n\2\2km\7\b\2\2lk\3\2\2\2mp\3\2\2\2nl\3\2\2\2no\3\2\2\2or\3\2\2\2"+
		"pn\3\2\2\2qO\3\2\2\2qV\3\2\2\2qf\3\2\2\2r\13\3\2\2\2st\5\n\6\2tv\7*\2"+
		"\2uw\7\"\2\2vu\3\2\2\2vw\3\2\2\2wy\3\2\2\2xz\7#\2\2yx\3\2\2\2yz\3\2\2"+
		"\2z}\3\2\2\2{|\7\27\2\2|~\5&\24\2}{\3\2\2\2}~\3\2\2\2~\177\3\2\2\2\177"+
		"\u0080\7\13\2\2\u0080\r\3\2\2\2\u0081\u0082\5\n\6\2\u0082\u0085\7*\2\2"+
		"\u0083\u0084\7\27\2\2\u0084\u0086\5&\24\2\u0085\u0083\3\2\2\2\u0085\u0086"+
		"\3\2\2\2\u0086\17\3\2\2\2\u0087\u0088\5\n\6\2\u0088\u0089\7*\2\2\u0089"+
		"\u0092\7\6\2\2\u008a\u008f\5\16\b\2\u008b\u008c\7$\2\2\u008c\u008e\5\16"+
		"\b\2\u008d\u008b\3\2\2\2\u008e\u0091\3\2\2\2\u008f\u008d\3\2\2\2\u008f"+
		"\u0090\3\2\2\2\u0090\u0093\3\2\2\2\u0091\u008f\3\2\2\2\u0092\u008a\3\2"+
		"\2\2\u0092\u0093\3\2\2\2\u0093\u0094\3\2\2\2\u0094\u0095\7\7\2\2\u0095"+
		"\u0096\7\4\2\2\u0096\u0097\5\"\22\2\u0097\u0098\7\5\2\2\u0098\21\3\2\2"+
		"\2\u0099\u009a\5&\24\2\u009a\u009b\7\27\2\2\u009b\u009c\5&\24\2\u009c"+
		"\u009d\7\13\2\2\u009d\23\3\2\2\2\u009e\u009f\7%\2\2\u009f\u00a0\5&\24"+
		"\2\u00a0\u00a1\7\13\2\2\u00a1\25\3\2\2\2\u00a2\u00a3\5\n\6\2\u00a3\u00a6"+
		"\7*\2\2\u00a4\u00a5\7\27\2\2\u00a5\u00a7\5&\24\2\u00a6\u00a4\3\2\2\2\u00a6"+
		"\u00a7\3\2\2\2\u00a7\u00a8\3\2\2\2\u00a8\u00a9\7\13\2\2\u00a9\27\3\2\2"+
		"\2\u00aa\u00ab\7\21\2\2\u00ab\u00ac\7\6\2\2\u00ac\u00ad\5\62\32\2\u00ad"+
		"\u00ae\7\7\2\2\u00ae\u00af\7\4\2\2\u00af\u00b0\5\"\22\2\u00b0\u00b1\7"+
		"\5\2\2\u00b1\u00b2\7\22\2\2\u00b2\u00b3\7\4\2\2\u00b3\u00b4\5\"\22\2\u00b4"+
		"\u00b5\7\5\2\2\u00b5\31\3\2\2\2\u00b6\u00b7\7\24\2\2\u00b7\u00b8\7\6\2"+
		"\2\u00b8\u00b9\5\62\32\2\u00b9\u00ba\7\7\2\2\u00ba\u00bb\7\4\2\2\u00bb"+
		"\u00bc\5\"\22\2\u00bc\u00bd\7\5\2\2\u00bd\33\3\2\2\2\u00be\u00c1\5\22"+
		"\n\2\u00bf\u00c1\5\26\f\2\u00c0\u00be\3\2\2\2\u00c0\u00bf\3\2\2\2\u00c1"+
		"\35\3\2\2\2\u00c2\u00c3\7\25\2\2\u00c3\u00c4\7\6\2\2\u00c4\u00c5\5\34"+
		"\17\2\u00c5\u00c6\5\62\32\2\u00c6\u00c7\7\13\2\2\u00c7\u00c8\5&\24\2\u00c8"+
		"\u00c9\7\7\2\2\u00c9\u00ca\7\4\2\2\u00ca\u00cb\5\"\22\2\u00cb\u00cc\7"+
		"\5\2\2\u00cc\37\3\2\2\2\u00cd\u00ce\7*\2\2\u00ce\u00d7\7\6\2\2\u00cf\u00d4"+
		"\5&\24\2\u00d0\u00d1\7$\2\2\u00d1\u00d3\5&\24\2\u00d2\u00d0\3\2\2\2\u00d3"+
		"\u00d6\3\2\2\2\u00d4\u00d2\3\2\2\2\u00d4\u00d5\3\2\2\2\u00d5\u00d8\3\2"+
		"\2\2\u00d6\u00d4\3\2\2\2\u00d7\u00cf\3\2\2\2\u00d7\u00d8\3\2\2\2\u00d8"+
		"\u00d9\3\2\2\2\u00d9\u00da\7\7\2\2\u00da\u00db\7\13\2\2\u00db!\3\2\2\2"+
		"\u00dc\u00de\5$\23\2\u00dd\u00dc\3\2\2\2\u00de\u00e1\3\2\2\2\u00df\u00dd"+
		"\3\2\2\2\u00df\u00e0\3\2\2\2\u00e0#\3\2\2\2\u00e1\u00df\3\2\2\2\u00e2"+
		"\u00eb\5\22\n\2\u00e3\u00eb\5\24\13\2\u00e4\u00eb\5\20\t\2\u00e5\u00eb"+
		"\5\26\f\2\u00e6\u00eb\5\30\r\2\u00e7\u00eb\5\32\16\2\u00e8\u00eb\5\36"+
		"\20\2\u00e9\u00eb\5 \21\2\u00ea\u00e2\3\2\2\2\u00ea\u00e3\3\2\2\2\u00ea"+
		"\u00e4\3\2\2\2\u00ea\u00e5\3\2\2\2\u00ea\u00e6\3\2\2\2\u00ea\u00e7\3\2"+
		"\2\2\u00ea\u00e8\3\2\2\2\u00ea\u00e9\3\2\2\2\u00eb%\3\2\2\2\u00ec\u00ed"+
		"\b\24\1\2\u00ed\u00ee\7\6\2\2\u00ee\u00f1\5&\24\2\u00ef\u00f0\7$\2\2\u00f0"+
		"\u00f2\5&\24\2\u00f1\u00ef\3\2\2\2\u00f2\u00f3\3\2\2\2\u00f3\u00f1\3\2"+
		"\2\2\u00f3\u00f4\3\2\2\2\u00f4\u00f5\3\2\2\2\u00f5\u00f6\7\7\2\2\u00f6"+
		"\u0118\3\2\2\2\u00f7\u00f8\7\6\2\2\u00f8\u00f9\5&\24\2\u00f9\u00fa\7\7"+
		"\2\2\u00fa\u0118\3\2\2\2\u00fb\u00fc\7\t\2\2\u00fc\u0101\5&\24\2\u00fd"+
		"\u00fe\7$\2\2\u00fe\u0100\5&\24\2\u00ff\u00fd\3\2\2\2\u0100\u0103\3\2"+
		"\2\2\u0101\u00ff\3\2\2\2\u0101\u0102\3\2\2\2\u0102\u0104\3\2\2\2\u0103"+
		"\u0101\3\2\2\2\u0104\u0105\7\n\2\2\u0105\u0118\3\2\2\2\u0106\u0107\7\t"+
		"\2\2\u0107\u0108\5&\24\2\u0108\u0109\7\f\2\2\u0109\u0111\5&\24\2\u010a"+
		"\u010b\7$\2\2\u010b\u010c\5&\24\2\u010c\u010d\7\f\2\2\u010d\u010e\5&\24"+
		"\2\u010e\u0110\3\2\2\2\u010f\u010a\3\2\2\2\u0110\u0113\3\2\2\2\u0111\u010f"+
		"\3\2\2\2\u0111\u0112\3\2\2\2\u0112\u0114\3\2\2\2\u0113\u0111\3\2\2\2\u0114"+
		"\u0115\7\n\2\2\u0115\u0118\3\2\2\2\u0116\u0118\5\60\31\2\u0117\u00ec\3"+
		"\2\2\2\u0117\u00f7\3\2\2\2\u0117\u00fb\3\2\2\2\u0117\u0106\3\2\2\2\u0117"+
		"\u0116\3\2\2\2\u0118\u012d\3\2\2\2\u0119\u011a\f\r\2\2\u011a\u011b\7 "+
		"\2\2\u011b\u012c\5&\24\16\u011c\u011d\f\f\2\2\u011d\u011e\7!\2\2\u011e"+
		"\u012c\5&\24\r\u011f\u0120\f\13\2\2\u0120\u0121\7\36\2\2\u0121\u012c\5"+
		"&\24\f\u0122\u0123\f\n\2\2\u0123\u0124\7\37\2\2\u0124\u012c\5&\24\13\u0125"+
		"\u0126\f\5\2\2\u0126\u0127\7&\2\2\u0127\u012c\5&\24\6\u0128\u0129\f\4"+
		"\2\2\u0129\u012a\7\27\2\2\u012a\u012c\5&\24\5\u012b\u0119\3\2\2\2\u012b"+
		"\u011c\3\2\2\2\u012b\u011f\3\2\2\2\u012b\u0122\3\2\2\2\u012b\u0125\3\2"+
		"\2\2\u012b\u0128\3\2\2\2\u012c\u012f\3\2\2\2\u012d\u012b\3\2\2\2\u012d"+
		"\u012e\3\2\2\2\u012e\'\3\2\2\2\u012f\u012d\3\2\2\2\u0130\u0131\t\2\2\2"+
		"\u0131)\3\2\2\2\u0132\u0133\7*\2\2\u0133\u0134\7\t\2\2\u0134\u0135\5&"+
		"\24\2\u0135\u0136\7\n\2\2\u0136+\3\2\2\2\u0137\u0139\7\17\2\2\u0138\u013a"+
		"\7(\2\2\u0139\u0138\3\2\2\2\u0139\u013a\3\2\2\2\u013a\u0140\3\2\2\2\u013b"+
		"\u013d\7\20\2\2\u013c\u013e\7)\2\2\u013d\u013c\3\2\2\2\u013d\u013e\3\2"+
		"\2\2\u013e\u0140\3\2\2\2\u013f\u0137\3\2\2\2\u013f\u013b\3\2\2\2\u0140"+
		"-\3\2\2\2\u0141\u0145\7\26\2\2\u0142\u0144\13\2\2\2\u0143\u0142\3\2\2"+
		"\2\u0144\u0147\3\2\2\2\u0145\u0146\3\2\2\2\u0145\u0143\3\2\2\2\u0146\u0148"+
		"\3\2\2\2\u0147\u0145\3\2\2\2\u0148\u0149\7\26\2\2\u0149/\3\2\2\2\u014a"+
		"\u0152\5,\27\2\u014b\u0152\5(\25\2\u014c\u0152\7\r\2\2\u014d\u0152\7\16"+
		"\2\2\u014e\u0152\5.\30\2\u014f\u0152\5*\26\2\u0150\u0152\7*\2\2\u0151"+
		"\u014a\3\2\2\2\u0151\u014b\3\2\2\2\u0151\u014c\3\2\2\2\u0151\u014d\3\2"+
		"\2\2\u0151\u014e\3\2\2\2\u0151\u014f\3\2\2\2\u0151\u0150\3\2\2\2\u0152"+
		"\61\3\2\2\2\u0153\u0154\5&\24\2\u0154\u0155\7\27\2\2\u0155\u0156\5&\24"+
		"\2\u0156\u0168\3\2\2\2\u0157\u0158\5&\24\2\u0158\u0159\7\30\2\2\u0159"+
		"\u015a\5&\24\2\u015a\u0168\3\2\2\2\u015b\u015c\5&\24\2\u015c\u015d\7\31"+
		"\2\2\u015d\u015e\5&\24\2\u015e\u0168\3\2\2\2\u015f\u0160\5&\24\2\u0160"+
		"\u0161\7\32\2\2\u0161\u0162\5&\24\2\u0162\u0168\3\2\2\2\u0163\u0164\5"+
		"&\24\2\u0164\u0165\7\33\2\2\u0165\u0166\5&\24\2\u0166\u0168\3\2\2\2\u0167"+
		"\u0153\3\2\2\2\u0167\u0157\3\2\2\2\u0167\u015b\3\2\2\2\u0167\u015f\3\2"+
		"\2\2\u0167\u0163\3\2\2\2\u0168\63\3\2\2\2\u0169\u016a\5\n\6\2\u016a\u016b"+
		"\7*\2\2\u016b\u016c\7\'\2\2\u016c\u016d\7*\2\2\u016d\u0176\7\6\2\2\u016e"+
		"\u0173\5\16\b\2\u016f\u0170\7$\2\2\u0170\u0172\5\16\b\2\u0171\u016f\3"+
		"\2\2\2\u0172\u0175\3\2\2\2\u0173\u0171\3\2\2\2\u0173\u0174\3\2\2\2\u0174"+
		"\u0177\3\2\2\2\u0175\u0173\3\2\2\2\u0176\u016e\3\2\2\2\u0176\u0177\3\2"+
		"\2\2\u0177\u0178\3\2\2\2\u0178\u0179\7\7\2\2\u0179\u017a\7\4\2\2\u017a"+
		"\u017b\5\"\22\2\u017b\u017c\7\5\2\2\u017c\65\3\2\2\2%8=FMS\\cnqvy}\u0085"+
		"\u008f\u0092\u00a6\u00c0\u00d4\u00d7\u00df\u00ea\u00f3\u0101\u0111\u0117"+
		"\u012b\u012d\u0139\u013d\u013f\u0145\u0151\u0167\u0173\u0176";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}