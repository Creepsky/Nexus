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
		T__0=1, CLASS=2, LEFT_CURLY_BRACE=3, RIGHT_CURLY_BRACE=4, LEFT_BRACE=5, 
		RIGHT_BRACE=6, LEFT_BRACKET=7, RIGHT_BRACKET=8, COLON=9, ARROW_RIGHT=10, 
		BINARY=11, HEX=12, INTEGER=13, REAL=14, IF=15, ELSE=16, ELSE_IF=17, WHILE=18, 
		FOR=19, EQUAL=20, LESS=21, LESS_EQUAL=22, GREATER=23, GREATER_EQUAL=24, 
		QUOTED_TEXT=25, TRUE=26, FALSE=27, PLUS=28, MINUS=29, STAR=30, SLASH=31, 
		SET=32, GET=33, COMMA=34, RETURN=35, RANGE=36, DOT=37, INTEGER_SUFFIX=38, 
		REAL_SUFFIX=39, IDENTIFIER=40, WHITESPACE=41;
	public static final int
		RULE_file_declaration = 0, RULE_file = 1, RULE_class = 2, RULE_class_member = 3, 
		RULE_tuple_declaration = 4, RULE_type = 5, RULE_variable_declaration = 6, 
		RULE_function_parameter = 7, RULE_function_declaration = 8, RULE_assignment_statement = 9, 
		RULE_return_statement = 10, RULE_variable_statement = 11, RULE_if_statement = 12, 
		RULE_while_statement = 13, RULE_for_init = 14, RULE_for_statement = 15, 
		RULE_function_call_statement = 16, RULE_function_body = 17, RULE_expression = 18, 
		RULE_boolean = 19, RULE_array_access = 20, RULE_number = 21, RULE_factor = 22, 
		RULE_comparison = 23, RULE_extension_function = 24;
	public static final String[] ruleNames = {
		"file_declaration", "file", "class", "class_member", "tuple_declaration", 
		"type", "variable_declaration", "function_parameter", "function_declaration", 
		"assignment_statement", "return_statement", "variable_statement", "if_statement", 
		"while_statement", "for_init", "for_statement", "function_call_statement", 
		"function_body", "expression", "boolean", "array_access", "number", "factor", 
		"comparison", "extension_function"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'[]'", "'class'", "'{'", "'}'", "'('", "')'", "'['", "']'", "';'", 
		"'->'", null, null, null, null, "'if'", "'else'", null, "'while'", "'for'", 
		"'='", "'<'", "'<='", "'>'", "'>='", null, "'true'", "'false'", "'+'", 
		"'-'", "'*'", "'/'", "'set'", "'get'", "','", "'return'", "'..'", "'.'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, "CLASS", "LEFT_CURLY_BRACE", "RIGHT_CURLY_BRACE", "LEFT_BRACE", 
		"RIGHT_BRACE", "LEFT_BRACKET", "RIGHT_BRACKET", "COLON", "ARROW_RIGHT", 
		"BINARY", "HEX", "INTEGER", "REAL", "IF", "ELSE", "ELSE_IF", "WHILE", 
		"FOR", "EQUAL", "LESS", "LESS_EQUAL", "GREATER", "GREATER_EQUAL", "QUOTED_TEXT", 
		"TRUE", "FALSE", "PLUS", "MINUS", "STAR", "SLASH", "SET", "GET", "COMMA", 
		"RETURN", "RANGE", "DOT", "INTEGER_SUFFIX", "REAL_SUFFIX", "IDENTIFIER", 
		"WHITESPACE"
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
			setState(52);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case CLASS:
				enterOuterAlt(_localctx, 1);
				{
				setState(50);
				class();
				}
				break;
			case LEFT_BRACE:
			case LEFT_BRACKET:
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(51);
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
			setState(57);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << CLASS) | (1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(54);
				file_declaration();
				}
				}
				setState(59);
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
		public TerminalNode CLASS() { return getToken(NexusParser.CLASS, 0); }
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
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
			setState(60);
			match(CLASS);
			setState(61);
			match(IDENTIFIER);
			setState(62);
			match(LEFT_CURLY_BRACE);
			setState(66);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(63);
				class_member();
				}
				}
				setState(68);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(69);
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
			setState(73);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(71);
				variable_declaration();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(72);
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

	public static class Tuple_declarationContext extends ParserRuleContext {
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
		public Tuple_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_tuple_declaration; }
	}

	public final Tuple_declarationContext tuple_declaration() throws RecognitionException {
		Tuple_declarationContext _localctx = new Tuple_declarationContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_tuple_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(75);
			match(LEFT_BRACE);
			setState(76);
			type();
			setState(81);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(77);
				match(COMMA);
				setState(78);
				type();
				}
				}
				setState(83);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(84);
			match(RIGHT_BRACE);
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
		public TerminalNode IDENTIFIER() { return getToken(NexusParser.IDENTIFIER, 0); }
		public Tuple_declarationContext tuple_declaration() {
			return getRuleContext(Tuple_declarationContext.class,0);
		}
		public TerminalNode LEFT_BRACKET() { return getToken(NexusParser.LEFT_BRACKET, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TerminalNode ARROW_RIGHT() { return getToken(NexusParser.ARROW_RIGHT, 0); }
		public TerminalNode RIGHT_BRACKET() { return getToken(NexusParser.RIGHT_BRACKET, 0); }
		public List<TerminalNode> COMMA() { return getTokens(NexusParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(NexusParser.COMMA, i);
		}
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_type);
		int _la;
		try {
			setState(117);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IDENTIFIER:
				enterOuterAlt(_localctx, 1);
				{
				setState(86);
				match(IDENTIFIER);
				setState(90);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__0) {
					{
					{
					setState(87);
					match(T__0);
					}
					}
					setState(92);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case LEFT_BRACE:
				enterOuterAlt(_localctx, 2);
				{
				setState(93);
				tuple_declaration();
				setState(97);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__0) {
					{
					{
					setState(94);
					match(T__0);
					}
					}
					setState(99);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case LEFT_BRACKET:
				enterOuterAlt(_localctx, 3);
				{
				setState(100);
				match(LEFT_BRACKET);
				setState(101);
				type();
				setState(102);
				match(ARROW_RIGHT);
				setState(106);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(103);
					match(COMMA);
					}
					}
					setState(108);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(109);
				type();
				setState(110);
				match(RIGHT_BRACKET);
				setState(114);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__0) {
					{
					{
					setState(111);
					match(T__0);
					}
					}
					setState(116);
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
		enterRule(_localctx, 12, RULE_variable_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(119);
			type();
			setState(120);
			match(IDENTIFIER);
			setState(122);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==SET) {
				{
				setState(121);
				match(SET);
				}
			}

			setState(125);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==GET) {
				{
				setState(124);
				match(GET);
				}
			}

			setState(129);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(127);
				match(EQUAL);
				setState(128);
				expression(0);
				}
			}

			setState(131);
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
		enterRule(_localctx, 14, RULE_function_parameter);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			type();
			setState(134);
			match(IDENTIFIER);
			setState(137);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(135);
				match(EQUAL);
				setState(136);
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
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<Function_parameterContext> function_parameter() {
			return getRuleContexts(Function_parameterContext.class);
		}
		public Function_parameterContext function_parameter(int i) {
			return getRuleContext(Function_parameterContext.class,i);
		}
		public List<Function_bodyContext> function_body() {
			return getRuleContexts(Function_bodyContext.class);
		}
		public Function_bodyContext function_body(int i) {
			return getRuleContext(Function_bodyContext.class,i);
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
		enterRule(_localctx, 16, RULE_function_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(139);
			type();
			setState(140);
			match(IDENTIFIER);
			setState(141);
			match(LEFT_BRACE);
			setState(150);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(142);
				function_parameter();
				setState(147);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(143);
					match(COMMA);
					setState(144);
					function_parameter();
					}
					}
					setState(149);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(152);
			match(RIGHT_BRACE);
			setState(153);
			match(LEFT_CURLY_BRACE);
			setState(157);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(154);
				function_body();
				}
				}
				setState(159);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(160);
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
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode EQUAL() { return getToken(NexusParser.EQUAL, 0); }
		public TerminalNode COLON() { return getToken(NexusParser.COLON, 0); }
		public Assignment_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment_statement; }
	}

	public final Assignment_statementContext assignment_statement() throws RecognitionException {
		Assignment_statementContext _localctx = new Assignment_statementContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_assignment_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(162);
			expression(0);
			setState(163);
			match(EQUAL);
			setState(164);
			expression(0);
			setState(165);
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
		enterRule(_localctx, 20, RULE_return_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			match(RETURN);
			setState(168);
			expression(0);
			setState(169);
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
		enterRule(_localctx, 22, RULE_variable_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(171);
			type();
			setState(172);
			match(IDENTIFIER);
			setState(175);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EQUAL) {
				{
				setState(173);
				match(EQUAL);
				setState(174);
				expression(0);
				}
			}

			setState(177);
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
		enterRule(_localctx, 24, RULE_if_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(179);
			match(IF);
			setState(180);
			match(LEFT_BRACE);
			setState(181);
			comparison();
			setState(182);
			match(RIGHT_BRACE);
			setState(183);
			match(LEFT_CURLY_BRACE);
			setState(187);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(184);
				function_body();
				}
				}
				setState(189);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(190);
			match(RIGHT_CURLY_BRACE);
			setState(191);
			match(ELSE);
			setState(192);
			match(LEFT_CURLY_BRACE);
			setState(196);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(193);
				function_body();
				}
				}
				setState(198);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(199);
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
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<Function_bodyContext> function_body() {
			return getRuleContexts(Function_bodyContext.class);
		}
		public Function_bodyContext function_body(int i) {
			return getRuleContext(Function_bodyContext.class,i);
		}
		public While_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_while_statement; }
	}

	public final While_statementContext while_statement() throws RecognitionException {
		While_statementContext _localctx = new While_statementContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_while_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(201);
			match(WHILE);
			setState(202);
			match(LEFT_BRACE);
			setState(203);
			comparison();
			setState(204);
			match(RIGHT_BRACE);
			setState(205);
			match(LEFT_CURLY_BRACE);
			setState(209);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(206);
				function_body();
				}
				}
				setState(211);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(212);
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
		enterRule(_localctx, 28, RULE_for_init);
		try {
			setState(216);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,21,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(214);
				assignment_statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(215);
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
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<Function_bodyContext> function_body() {
			return getRuleContexts(Function_bodyContext.class);
		}
		public Function_bodyContext function_body(int i) {
			return getRuleContext(Function_bodyContext.class,i);
		}
		public For_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_for_statement; }
	}

	public final For_statementContext for_statement() throws RecognitionException {
		For_statementContext _localctx = new For_statementContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_for_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(218);
			match(FOR);
			setState(219);
			match(LEFT_BRACE);
			setState(220);
			for_init();
			setState(221);
			comparison();
			setState(222);
			match(COLON);
			setState(223);
			expression(0);
			setState(224);
			match(RIGHT_BRACE);
			setState(225);
			match(LEFT_CURLY_BRACE);
			setState(229);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(226);
				function_body();
				}
				}
				setState(231);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(232);
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
		enterRule(_localctx, 32, RULE_function_call_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(234);
			match(IDENTIFIER);
			setState(235);
			match(LEFT_BRACE);
			setState(244);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(236);
				expression(0);
				setState(241);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(237);
					match(COMMA);
					setState(238);
					expression(0);
					}
					}
					setState(243);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(246);
			match(RIGHT_BRACE);
			setState(247);
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
		public Function_bodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body; }
	}

	public final Function_bodyContext function_body() throws RecognitionException {
		Function_bodyContext _localctx = new Function_bodyContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_function_body);
		try {
			setState(257);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(249);
				assignment_statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(250);
				return_statement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(251);
				function_declaration();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(252);
				variable_statement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(253);
				if_statement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(254);
				while_statement();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(255);
				for_statement();
				}
				break;
			case 8:
				enterOuterAlt(_localctx, 8);
				{
				setState(256);
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
			setState(302);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,29,_ctx) ) {
			case 1:
				{
				setState(260);
				match(LEFT_BRACE);
				setState(261);
				expression(0);
				setState(264); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(262);
					match(COMMA);
					setState(263);
					expression(0);
					}
					}
					setState(266); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==COMMA );
				setState(268);
				match(RIGHT_BRACE);
				}
				break;
			case 2:
				{
				setState(270);
				match(LEFT_BRACE);
				setState(271);
				expression(0);
				setState(272);
				match(RIGHT_BRACE);
				}
				break;
			case 3:
				{
				setState(274);
				match(LEFT_BRACKET);
				setState(275);
				expression(0);
				setState(280);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(276);
					match(COMMA);
					setState(277);
					expression(0);
					}
					}
					setState(282);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(283);
				match(RIGHT_BRACKET);
				}
				break;
			case 4:
				{
				setState(285);
				match(LEFT_BRACKET);
				setState(286);
				expression(0);
				setState(287);
				match(ARROW_RIGHT);
				setState(288);
				expression(0);
				setState(296);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(289);
					match(COMMA);
					setState(290);
					expression(0);
					setState(291);
					match(ARROW_RIGHT);
					setState(292);
					expression(0);
					}
					}
					setState(298);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(299);
				match(RIGHT_BRACKET);
				}
				break;
			case 5:
				{
				setState(301);
				factor();
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(324);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,31,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(322);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,30,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(304);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(305);
						match(STAR);
						setState(306);
						expression(12);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(307);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(308);
						match(SLASH);
						setState(309);
						expression(11);
						}
						break;
					case 3:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(310);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(311);
						match(PLUS);
						setState(312);
						expression(10);
						}
						break;
					case 4:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(313);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(314);
						match(MINUS);
						setState(315);
						expression(9);
						}
						break;
					case 5:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(316);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(317);
						match(RANGE);
						setState(318);
						expression(4);
						}
						break;
					case 6:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(319);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(320);
						match(EQUAL);
						setState(321);
						expression(3);
						}
						break;
					}
					} 
				}
				setState(326);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,31,_ctx);
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
			setState(327);
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
			setState(329);
			match(IDENTIFIER);
			setState(330);
			match(LEFT_BRACKET);
			setState(331);
			expression(0);
			setState(332);
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
			setState(342);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INTEGER:
				enterOuterAlt(_localctx, 1);
				{
				setState(334);
				match(INTEGER);
				setState(336);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
				case 1:
					{
					setState(335);
					match(INTEGER_SUFFIX);
					}
					break;
				}
				}
				break;
			case REAL:
				enterOuterAlt(_localctx, 2);
				{
				setState(338);
				match(REAL);
				setState(340);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
				case 1:
					{
					setState(339);
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

	public static class FactorContext extends ParserRuleContext {
		public NumberContext number() {
			return getRuleContext(NumberContext.class,0);
		}
		public BooleanContext boolean() {
			return getRuleContext(BooleanContext.class,0);
		}
		public TerminalNode BINARY() { return getToken(NexusParser.BINARY, 0); }
		public TerminalNode HEX() { return getToken(NexusParser.HEX, 0); }
		public TerminalNode QUOTED_TEXT() { return getToken(NexusParser.QUOTED_TEXT, 0); }
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
		enterRule(_localctx, 44, RULE_factor);
		try {
			setState(351);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,35,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(344);
				number();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(345);
				boolean();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(346);
				match(BINARY);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(347);
				match(HEX);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(348);
				match(QUOTED_TEXT);
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(349);
				array_access();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(350);
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
		enterRule(_localctx, 46, RULE_comparison);
		try {
			setState(373);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,36,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(353);
				expression(0);
				setState(354);
				match(EQUAL);
				setState(355);
				expression(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(357);
				expression(0);
				setState(358);
				match(LESS);
				setState(359);
				expression(0);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(361);
				expression(0);
				setState(362);
				match(LESS_EQUAL);
				setState(363);
				expression(0);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(365);
				expression(0);
				setState(366);
				match(GREATER);
				setState(367);
				expression(0);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(369);
				expression(0);
				setState(370);
				match(GREATER_EQUAL);
				setState(371);
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
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public List<TerminalNode> IDENTIFIER() { return getTokens(NexusParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(NexusParser.IDENTIFIER, i);
		}
		public TerminalNode DOT() { return getToken(NexusParser.DOT, 0); }
		public TerminalNode LEFT_BRACE() { return getToken(NexusParser.LEFT_BRACE, 0); }
		public TerminalNode RIGHT_BRACE() { return getToken(NexusParser.RIGHT_BRACE, 0); }
		public TerminalNode LEFT_CURLY_BRACE() { return getToken(NexusParser.LEFT_CURLY_BRACE, 0); }
		public TerminalNode RIGHT_CURLY_BRACE() { return getToken(NexusParser.RIGHT_CURLY_BRACE, 0); }
		public List<Function_parameterContext> function_parameter() {
			return getRuleContexts(Function_parameterContext.class);
		}
		public Function_parameterContext function_parameter(int i) {
			return getRuleContext(Function_parameterContext.class,i);
		}
		public List<Function_bodyContext> function_body() {
			return getRuleContexts(Function_bodyContext.class);
		}
		public Function_bodyContext function_body(int i) {
			return getRuleContext(Function_bodyContext.class,i);
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
		enterRule(_localctx, 48, RULE_extension_function);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(375);
			type();
			setState(376);
			match(IDENTIFIER);
			setState(377);
			match(DOT);
			setState(378);
			match(IDENTIFIER);
			setState(379);
			match(LEFT_BRACE);
			setState(388);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(380);
				function_parameter();
				setState(385);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMMA) {
					{
					{
					setState(381);
					match(COMMA);
					setState(382);
					function_parameter();
					}
					}
					setState(387);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(390);
			match(RIGHT_BRACE);
			setState(391);
			match(LEFT_CURLY_BRACE);
			setState(395);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_BRACE) | (1L << LEFT_BRACKET) | (1L << BINARY) | (1L << HEX) | (1L << INTEGER) | (1L << REAL) | (1L << IF) | (1L << WHILE) | (1L << FOR) | (1L << QUOTED_TEXT) | (1L << TRUE) | (1L << FALSE) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(392);
				function_body();
				}
				}
				setState(397);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(398);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3+\u0193\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\3\2\3\2\5\2\67\n\2\3\3\7\3:\n\3\f\3\16\3=\13\3\3\4\3\4\3\4"+
		"\3\4\7\4C\n\4\f\4\16\4F\13\4\3\4\3\4\3\5\3\5\5\5L\n\5\3\6\3\6\3\6\3\6"+
		"\7\6R\n\6\f\6\16\6U\13\6\3\6\3\6\3\7\3\7\7\7[\n\7\f\7\16\7^\13\7\3\7\3"+
		"\7\7\7b\n\7\f\7\16\7e\13\7\3\7\3\7\3\7\3\7\7\7k\n\7\f\7\16\7n\13\7\3\7"+
		"\3\7\3\7\7\7s\n\7\f\7\16\7v\13\7\5\7x\n\7\3\b\3\b\3\b\5\b}\n\b\3\b\5\b"+
		"\u0080\n\b\3\b\3\b\5\b\u0084\n\b\3\b\3\b\3\t\3\t\3\t\3\t\5\t\u008c\n\t"+
		"\3\n\3\n\3\n\3\n\3\n\3\n\7\n\u0094\n\n\f\n\16\n\u0097\13\n\5\n\u0099\n"+
		"\n\3\n\3\n\3\n\7\n\u009e\n\n\f\n\16\n\u00a1\13\n\3\n\3\n\3\13\3\13\3\13"+
		"\3\13\3\13\3\f\3\f\3\f\3\f\3\r\3\r\3\r\3\r\5\r\u00b2\n\r\3\r\3\r\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\7\16\u00bc\n\16\f\16\16\16\u00bf\13\16\3\16"+
		"\3\16\3\16\3\16\7\16\u00c5\n\16\f\16\16\16\u00c8\13\16\3\16\3\16\3\17"+
		"\3\17\3\17\3\17\3\17\3\17\7\17\u00d2\n\17\f\17\16\17\u00d5\13\17\3\17"+
		"\3\17\3\20\3\20\5\20\u00db\n\20\3\21\3\21\3\21\3\21\3\21\3\21\3\21\3\21"+
		"\3\21\7\21\u00e6\n\21\f\21\16\21\u00e9\13\21\3\21\3\21\3\22\3\22\3\22"+
		"\3\22\3\22\7\22\u00f2\n\22\f\22\16\22\u00f5\13\22\5\22\u00f7\n\22\3\22"+
		"\3\22\3\22\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\5\23\u0104\n\23\3\24"+
		"\3\24\3\24\3\24\3\24\6\24\u010b\n\24\r\24\16\24\u010c\3\24\3\24\3\24\3"+
		"\24\3\24\3\24\3\24\3\24\3\24\3\24\7\24\u0119\n\24\f\24\16\24\u011c\13"+
		"\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\7\24\u0129"+
		"\n\24\f\24\16\24\u012c\13\24\3\24\3\24\3\24\5\24\u0131\n\24\3\24\3\24"+
		"\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24"+
		"\3\24\3\24\7\24\u0145\n\24\f\24\16\24\u0148\13\24\3\25\3\25\3\26\3\26"+
		"\3\26\3\26\3\26\3\27\3\27\5\27\u0153\n\27\3\27\3\27\5\27\u0157\n\27\5"+
		"\27\u0159\n\27\3\30\3\30\3\30\3\30\3\30\3\30\3\30\5\30\u0162\n\30\3\31"+
		"\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31\3\31"+
		"\3\31\3\31\3\31\3\31\3\31\5\31\u0178\n\31\3\32\3\32\3\32\3\32\3\32\3\32"+
		"\3\32\3\32\7\32\u0182\n\32\f\32\16\32\u0185\13\32\5\32\u0187\n\32\3\32"+
		"\3\32\3\32\7\32\u018c\n\32\f\32\16\32\u018f\13\32\3\32\3\32\3\32\2\3&"+
		"\33\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\2\3\3\2\34\35"+
		"\2\u01b7\2\66\3\2\2\2\4;\3\2\2\2\6>\3\2\2\2\bK\3\2\2\2\nM\3\2\2\2\fw\3"+
		"\2\2\2\16y\3\2\2\2\20\u0087\3\2\2\2\22\u008d\3\2\2\2\24\u00a4\3\2\2\2"+
		"\26\u00a9\3\2\2\2\30\u00ad\3\2\2\2\32\u00b5\3\2\2\2\34\u00cb\3\2\2\2\36"+
		"\u00da\3\2\2\2 \u00dc\3\2\2\2\"\u00ec\3\2\2\2$\u0103\3\2\2\2&\u0130\3"+
		"\2\2\2(\u0149\3\2\2\2*\u014b\3\2\2\2,\u0158\3\2\2\2.\u0161\3\2\2\2\60"+
		"\u0177\3\2\2\2\62\u0179\3\2\2\2\64\67\5\6\4\2\65\67\5\62\32\2\66\64\3"+
		"\2\2\2\66\65\3\2\2\2\67\3\3\2\2\28:\5\2\2\298\3\2\2\2:=\3\2\2\2;9\3\2"+
		"\2\2;<\3\2\2\2<\5\3\2\2\2=;\3\2\2\2>?\7\4\2\2?@\7*\2\2@D\7\5\2\2AC\5\b"+
		"\5\2BA\3\2\2\2CF\3\2\2\2DB\3\2\2\2DE\3\2\2\2EG\3\2\2\2FD\3\2\2\2GH\7\6"+
		"\2\2H\7\3\2\2\2IL\5\16\b\2JL\5\22\n\2KI\3\2\2\2KJ\3\2\2\2L\t\3\2\2\2M"+
		"N\7\7\2\2NS\5\f\7\2OP\7$\2\2PR\5\f\7\2QO\3\2\2\2RU\3\2\2\2SQ\3\2\2\2S"+
		"T\3\2\2\2TV\3\2\2\2US\3\2\2\2VW\7\b\2\2W\13\3\2\2\2X\\\7*\2\2Y[\7\3\2"+
		"\2ZY\3\2\2\2[^\3\2\2\2\\Z\3\2\2\2\\]\3\2\2\2]x\3\2\2\2^\\\3\2\2\2_c\5"+
		"\n\6\2`b\7\3\2\2a`\3\2\2\2be\3\2\2\2ca\3\2\2\2cd\3\2\2\2dx\3\2\2\2ec\3"+
		"\2\2\2fg\7\t\2\2gh\5\f\7\2hl\7\f\2\2ik\7$\2\2ji\3\2\2\2kn\3\2\2\2lj\3"+
		"\2\2\2lm\3\2\2\2mo\3\2\2\2nl\3\2\2\2op\5\f\7\2pt\7\n\2\2qs\7\3\2\2rq\3"+
		"\2\2\2sv\3\2\2\2tr\3\2\2\2tu\3\2\2\2ux\3\2\2\2vt\3\2\2\2wX\3\2\2\2w_\3"+
		"\2\2\2wf\3\2\2\2x\r\3\2\2\2yz\5\f\7\2z|\7*\2\2{}\7\"\2\2|{\3\2\2\2|}\3"+
		"\2\2\2}\177\3\2\2\2~\u0080\7#\2\2\177~\3\2\2\2\177\u0080\3\2\2\2\u0080"+
		"\u0083\3\2\2\2\u0081\u0082\7\26\2\2\u0082\u0084\5&\24\2\u0083\u0081\3"+
		"\2\2\2\u0083\u0084\3\2\2\2\u0084\u0085\3\2\2\2\u0085\u0086\7\13\2\2\u0086"+
		"\17\3\2\2\2\u0087\u0088\5\f\7\2\u0088\u008b\7*\2\2\u0089\u008a\7\26\2"+
		"\2\u008a\u008c\5&\24\2\u008b\u0089\3\2\2\2\u008b\u008c\3\2\2\2\u008c\21"+
		"\3\2\2\2\u008d\u008e\5\f\7\2\u008e\u008f\7*\2\2\u008f\u0098\7\7\2\2\u0090"+
		"\u0095\5\20\t\2\u0091\u0092\7$\2\2\u0092\u0094\5\20\t\2\u0093\u0091\3"+
		"\2\2\2\u0094\u0097\3\2\2\2\u0095\u0093\3\2\2\2\u0095\u0096\3\2\2\2\u0096"+
		"\u0099\3\2\2\2\u0097\u0095\3\2\2\2\u0098\u0090\3\2\2\2\u0098\u0099\3\2"+
		"\2\2\u0099\u009a\3\2\2\2\u009a\u009b\7\b\2\2\u009b\u009f\7\5\2\2\u009c"+
		"\u009e\5$\23\2\u009d\u009c\3\2\2\2\u009e\u00a1\3\2\2\2\u009f\u009d\3\2"+
		"\2\2\u009f\u00a0\3\2\2\2\u00a0\u00a2\3\2\2\2\u00a1\u009f\3\2\2\2\u00a2"+
		"\u00a3\7\6\2\2\u00a3\23\3\2\2\2\u00a4\u00a5\5&\24\2\u00a5\u00a6\7\26\2"+
		"\2\u00a6\u00a7\5&\24\2\u00a7\u00a8\7\13\2\2\u00a8\25\3\2\2\2\u00a9\u00aa"+
		"\7%\2\2\u00aa\u00ab\5&\24\2\u00ab\u00ac\7\13\2\2\u00ac\27\3\2\2\2\u00ad"+
		"\u00ae\5\f\7\2\u00ae\u00b1\7*\2\2\u00af\u00b0\7\26\2\2\u00b0\u00b2\5&"+
		"\24\2\u00b1\u00af\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2\u00b3\3\2\2\2\u00b3"+
		"\u00b4\7\13\2\2\u00b4\31\3\2\2\2\u00b5\u00b6\7\21\2\2\u00b6\u00b7\7\7"+
		"\2\2\u00b7\u00b8\5\60\31\2\u00b8\u00b9\7\b\2\2\u00b9\u00bd\7\5\2\2\u00ba"+
		"\u00bc\5$\23\2\u00bb\u00ba\3\2\2\2\u00bc\u00bf\3\2\2\2\u00bd\u00bb\3\2"+
		"\2\2\u00bd\u00be\3\2\2\2\u00be\u00c0\3\2\2\2\u00bf\u00bd\3\2\2\2\u00c0"+
		"\u00c1\7\6\2\2\u00c1\u00c2\7\22\2\2\u00c2\u00c6\7\5\2\2\u00c3\u00c5\5"+
		"$\23\2\u00c4\u00c3\3\2\2\2\u00c5\u00c8\3\2\2\2\u00c6\u00c4\3\2\2\2\u00c6"+
		"\u00c7\3\2\2\2\u00c7\u00c9\3\2\2\2\u00c8\u00c6\3\2\2\2\u00c9\u00ca\7\6"+
		"\2\2\u00ca\33\3\2\2\2\u00cb\u00cc\7\24\2\2\u00cc\u00cd\7\7\2\2\u00cd\u00ce"+
		"\5\60\31\2\u00ce\u00cf\7\b\2\2\u00cf\u00d3\7\5\2\2\u00d0\u00d2\5$\23\2"+
		"\u00d1\u00d0\3\2\2\2\u00d2\u00d5\3\2\2\2\u00d3\u00d1\3\2\2\2\u00d3\u00d4"+
		"\3\2\2\2\u00d4\u00d6\3\2\2\2\u00d5\u00d3\3\2\2\2\u00d6\u00d7\7\6\2\2\u00d7"+
		"\35\3\2\2\2\u00d8\u00db\5\24\13\2\u00d9\u00db\5\30\r\2\u00da\u00d8\3\2"+
		"\2\2\u00da\u00d9\3\2\2\2\u00db\37\3\2\2\2\u00dc\u00dd\7\25\2\2\u00dd\u00de"+
		"\7\7\2\2\u00de\u00df\5\36\20\2\u00df\u00e0\5\60\31\2\u00e0\u00e1\7\13"+
		"\2\2\u00e1\u00e2\5&\24\2\u00e2\u00e3\7\b\2\2\u00e3\u00e7\7\5\2\2\u00e4"+
		"\u00e6\5$\23\2\u00e5\u00e4\3\2\2\2\u00e6\u00e9\3\2\2\2\u00e7\u00e5\3\2"+
		"\2\2\u00e7\u00e8\3\2\2\2\u00e8\u00ea\3\2\2\2\u00e9\u00e7\3\2\2\2\u00ea"+
		"\u00eb\7\6\2\2\u00eb!\3\2\2\2\u00ec\u00ed\7*\2\2\u00ed\u00f6\7\7\2\2\u00ee"+
		"\u00f3\5&\24\2\u00ef\u00f0\7$\2\2\u00f0\u00f2\5&\24\2\u00f1\u00ef\3\2"+
		"\2\2\u00f2\u00f5\3\2\2\2\u00f3\u00f1\3\2\2\2\u00f3\u00f4\3\2\2\2\u00f4"+
		"\u00f7\3\2\2\2\u00f5\u00f3\3\2\2\2\u00f6\u00ee\3\2\2\2\u00f6\u00f7\3\2"+
		"\2\2\u00f7\u00f8\3\2\2\2\u00f8\u00f9\7\b\2\2\u00f9\u00fa\7\13\2\2\u00fa"+
		"#\3\2\2\2\u00fb\u0104\5\24\13\2\u00fc\u0104\5\26\f\2\u00fd\u0104\5\22"+
		"\n\2\u00fe\u0104\5\30\r\2\u00ff\u0104\5\32\16\2\u0100\u0104\5\34\17\2"+
		"\u0101\u0104\5 \21\2\u0102\u0104\5\"\22\2\u0103\u00fb\3\2\2\2\u0103\u00fc"+
		"\3\2\2\2\u0103\u00fd\3\2\2\2\u0103\u00fe\3\2\2\2\u0103\u00ff\3\2\2\2\u0103"+
		"\u0100\3\2\2\2\u0103\u0101\3\2\2\2\u0103\u0102\3\2\2\2\u0104%\3\2\2\2"+
		"\u0105\u0106\b\24\1\2\u0106\u0107\7\7\2\2\u0107\u010a\5&\24\2\u0108\u0109"+
		"\7$\2\2\u0109\u010b\5&\24\2\u010a\u0108\3\2\2\2\u010b\u010c\3\2\2\2\u010c"+
		"\u010a\3\2\2\2\u010c\u010d\3\2\2\2\u010d\u010e\3\2\2\2\u010e\u010f\7\b"+
		"\2\2\u010f\u0131\3\2\2\2\u0110\u0111\7\7\2\2\u0111\u0112\5&\24\2\u0112"+
		"\u0113\7\b\2\2\u0113\u0131\3\2\2\2\u0114\u0115\7\t\2\2\u0115\u011a\5&"+
		"\24\2\u0116\u0117\7$\2\2\u0117\u0119\5&\24\2\u0118\u0116\3\2\2\2\u0119"+
		"\u011c\3\2\2\2\u011a\u0118\3\2\2\2\u011a\u011b\3\2\2\2\u011b\u011d\3\2"+
		"\2\2\u011c\u011a\3\2\2\2\u011d\u011e\7\n\2\2\u011e\u0131\3\2\2\2\u011f"+
		"\u0120\7\t\2\2\u0120\u0121\5&\24\2\u0121\u0122\7\f\2\2\u0122\u012a\5&"+
		"\24\2\u0123\u0124\7$\2\2\u0124\u0125\5&\24\2\u0125\u0126\7\f\2\2\u0126"+
		"\u0127\5&\24\2\u0127\u0129\3\2\2\2\u0128\u0123\3\2\2\2\u0129\u012c\3\2"+
		"\2\2\u012a\u0128\3\2\2\2\u012a\u012b\3\2\2\2\u012b\u012d\3\2\2\2\u012c"+
		"\u012a\3\2\2\2\u012d\u012e\7\n\2\2\u012e\u0131\3\2\2\2\u012f\u0131\5."+
		"\30\2\u0130\u0105\3\2\2\2\u0130\u0110\3\2\2\2\u0130\u0114\3\2\2\2\u0130"+
		"\u011f\3\2\2\2\u0130\u012f\3\2\2\2\u0131\u0146\3\2\2\2\u0132\u0133\f\r"+
		"\2\2\u0133\u0134\7 \2\2\u0134\u0145\5&\24\16\u0135\u0136\f\f\2\2\u0136"+
		"\u0137\7!\2\2\u0137\u0145\5&\24\r\u0138\u0139\f\13\2\2\u0139\u013a\7\36"+
		"\2\2\u013a\u0145\5&\24\f\u013b\u013c\f\n\2\2\u013c\u013d\7\37\2\2\u013d"+
		"\u0145\5&\24\13\u013e\u013f\f\5\2\2\u013f\u0140\7&\2\2\u0140\u0145\5&"+
		"\24\6\u0141\u0142\f\4\2\2\u0142\u0143\7\26\2\2\u0143\u0145\5&\24\5\u0144"+
		"\u0132\3\2\2\2\u0144\u0135\3\2\2\2\u0144\u0138\3\2\2\2\u0144\u013b\3\2"+
		"\2\2\u0144\u013e\3\2\2\2\u0144\u0141\3\2\2\2\u0145\u0148\3\2\2\2\u0146"+
		"\u0144\3\2\2\2\u0146\u0147\3\2\2\2\u0147\'\3\2\2\2\u0148\u0146\3\2\2\2"+
		"\u0149\u014a\t\2\2\2\u014a)\3\2\2\2\u014b\u014c\7*\2\2\u014c\u014d\7\t"+
		"\2\2\u014d\u014e\5&\24\2\u014e\u014f\7\n\2\2\u014f+\3\2\2\2\u0150\u0152"+
		"\7\17\2\2\u0151\u0153\7(\2\2\u0152\u0151\3\2\2\2\u0152\u0153\3\2\2\2\u0153"+
		"\u0159\3\2\2\2\u0154\u0156\7\20\2\2\u0155\u0157\7)\2\2\u0156\u0155\3\2"+
		"\2\2\u0156\u0157\3\2\2\2\u0157\u0159\3\2\2\2\u0158\u0150\3\2\2\2\u0158"+
		"\u0154\3\2\2\2\u0159-\3\2\2\2\u015a\u0162\5,\27\2\u015b\u0162\5(\25\2"+
		"\u015c\u0162\7\r\2\2\u015d\u0162\7\16\2\2\u015e\u0162\7\33\2\2\u015f\u0162"+
		"\5*\26\2\u0160\u0162\7*\2\2\u0161\u015a\3\2\2\2\u0161\u015b\3\2\2\2\u0161"+
		"\u015c\3\2\2\2\u0161\u015d\3\2\2\2\u0161\u015e\3\2\2\2\u0161\u015f\3\2"+
		"\2\2\u0161\u0160\3\2\2\2\u0162/\3\2\2\2\u0163\u0164\5&\24\2\u0164\u0165"+
		"\7\26\2\2\u0165\u0166\5&\24\2\u0166\u0178\3\2\2\2\u0167\u0168\5&\24\2"+
		"\u0168\u0169\7\27\2\2\u0169\u016a\5&\24\2\u016a\u0178\3\2\2\2\u016b\u016c"+
		"\5&\24\2\u016c\u016d\7\30\2\2\u016d\u016e\5&\24\2\u016e\u0178\3\2\2\2"+
		"\u016f\u0170\5&\24\2\u0170\u0171\7\31\2\2\u0171\u0172\5&\24\2\u0172\u0178"+
		"\3\2\2\2\u0173\u0174\5&\24\2\u0174\u0175\7\32\2\2\u0175\u0176\5&\24\2"+
		"\u0176\u0178\3\2\2\2\u0177\u0163\3\2\2\2\u0177\u0167\3\2\2\2\u0177\u016b"+
		"\3\2\2\2\u0177\u016f\3\2\2\2\u0177\u0173\3\2\2\2\u0178\61\3\2\2\2\u0179"+
		"\u017a\5\f\7\2\u017a\u017b\7*\2\2\u017b\u017c\7\'\2\2\u017c\u017d\7*\2"+
		"\2\u017d\u0186\7\7\2\2\u017e\u0183\5\20\t\2\u017f\u0180\7$\2\2\u0180\u0182"+
		"\5\20\t\2\u0181\u017f\3\2\2\2\u0182\u0185\3\2\2\2\u0183\u0181\3\2\2\2"+
		"\u0183\u0184\3\2\2\2\u0184\u0187\3\2\2\2\u0185\u0183\3\2\2\2\u0186\u017e"+
		"\3\2\2\2\u0186\u0187\3\2\2\2\u0187\u0188\3\2\2\2\u0188\u0189\7\b\2\2\u0189"+
		"\u018d\7\5\2\2\u018a\u018c\5$\23\2\u018b\u018a\3\2\2\2\u018c\u018f\3\2"+
		"\2\2\u018d\u018b\3\2\2\2\u018d\u018e\3\2\2\2\u018e\u0190\3\2\2\2\u018f"+
		"\u018d\3\2\2\2\u0190\u0191\7\6\2\2\u0191\63\3\2\2\2*\66;DKS\\cltw|\177"+
		"\u0083\u008b\u0095\u0098\u009f\u00b1\u00bd\u00c6\u00d3\u00da\u00e7\u00f3"+
		"\u00f6\u0103\u010c\u011a\u012a\u0130\u0144\u0146\u0152\u0156\u0158\u0161"+
		"\u0177\u0183\u0186\u018d";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}