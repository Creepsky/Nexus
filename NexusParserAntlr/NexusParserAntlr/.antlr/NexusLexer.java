// Generated from e:\dev\Volt\NexusParserAntlr\NexusParserAntlr\Nexus.gp4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class NexusLexer extends Lexer {
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
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "CLASS", "LEFT_CURLY_BRACE", "RIGHT_CURLY_BRACE", "LEFT_BRACE", 
		"RIGHT_BRACE", "LEFT_BRACKET", "RIGHT_BRACKET", "COLON", "ARROW_RIGHT", 
		"BINARY", "HEX", "INTEGER", "REAL", "IF", "ELSE", "ELSE_IF", "WHILE", 
		"FOR", "EQUAL", "LESS", "LESS_EQUAL", "GREATER", "GREATER_EQUAL", "QUOTED_TEXT", 
		"TRUE", "FALSE", "PLUS", "MINUS", "STAR", "SLASH", "SET", "GET", "COMMA", 
		"RETURN", "RANGE", "DOT", "INTEGER_SUFFIX", "REAL_SUFFIX", "IDENTIFIER", 
		"WHITESPACE"
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


	public NexusLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Nexus.gp4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2+\u0118\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\3\2\3\2"+
		"\3\2\3\3\3\3\3\3\3\3\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\b\3\b\3"+
		"\t\3\t\3\n\3\n\3\13\3\13\3\13\3\f\3\f\3\f\6\fs\n\f\r\f\16\ft\3\r\3\r\3"+
		"\r\3\r\6\r{\n\r\r\r\16\r|\3\16\5\16\u0080\n\16\3\16\6\16\u0083\n\16\r"+
		"\16\16\16\u0084\3\16\3\16\6\16\u0089\n\16\r\16\16\16\u008a\7\16\u008d"+
		"\n\16\f\16\16\16\u0090\13\16\3\17\5\17\u0093\n\17\3\17\6\17\u0096\n\17"+
		"\r\17\16\17\u0097\3\17\3\17\7\17\u009c\n\17\f\17\16\17\u009f\13\17\3\20"+
		"\3\20\3\20\3\21\3\21\3\21\3\21\3\21\3\22\3\22\3\22\3\23\3\23\3\23\3\23"+
		"\3\23\3\23\3\24\3\24\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\27\3\30"+
		"\3\30\3\31\3\31\3\31\3\32\3\32\7\32\u00c4\n\32\f\32\16\32\u00c7\13\32"+
		"\3\32\3\32\3\33\3\33\3\33\3\33\3\33\3\34\3\34\3\34\3\34\3\34\3\34\3\35"+
		"\3\35\3\36\3\36\3\37\3\37\3 \3 \3!\3!\3!\3!\3\"\3\"\3\"\3\"\3#\3#\3$\3"+
		"$\3$\3$\3$\3$\3$\3%\3%\3%\3&\3&\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\5"+
		"\'\u00fd\n\'\3(\3(\3(\3(\3(\3(\3(\5(\u0106\n(\3(\5(\u0109\n(\3)\3)\7)"+
		"\u010d\n)\f)\16)\u0110\13)\3*\6*\u0113\n*\r*\16*\u0114\3*\3*\3\u00c5\2"+
		"+\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20"+
		"\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67\359\36;\37"+
		"= ?!A\"C#E$G%I&K\'M(O)Q*S+\3\2\13\4\2DDdd\4\2\62;CH\4\2--//\3\2\62;\4"+
		"\2kkww\4\2ffhh\6\2//C\\aac|\7\2//\62;C\\aac|\5\2\13\f\17\17\"\"\2\u0128"+
		"\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2"+
		"\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2"+
		"\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2"+
		"\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2"+
		"\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3"+
		"\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2"+
		"\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\3"+
		"U\3\2\2\2\5X\3\2\2\2\7^\3\2\2\2\t`\3\2\2\2\13b\3\2\2\2\rd\3\2\2\2\17f"+
		"\3\2\2\2\21h\3\2\2\2\23j\3\2\2\2\25l\3\2\2\2\27o\3\2\2\2\31v\3\2\2\2\33"+
		"\177\3\2\2\2\35\u0092\3\2\2\2\37\u00a0\3\2\2\2!\u00a3\3\2\2\2#\u00a8\3"+
		"\2\2\2%\u00ab\3\2\2\2\'\u00b1\3\2\2\2)\u00b5\3\2\2\2+\u00b7\3\2\2\2-\u00b9"+
		"\3\2\2\2/\u00bc\3\2\2\2\61\u00be\3\2\2\2\63\u00c1\3\2\2\2\65\u00ca\3\2"+
		"\2\2\67\u00cf\3\2\2\29\u00d5\3\2\2\2;\u00d7\3\2\2\2=\u00d9\3\2\2\2?\u00db"+
		"\3\2\2\2A\u00dd\3\2\2\2C\u00e1\3\2\2\2E\u00e5\3\2\2\2G\u00e7\3\2\2\2I"+
		"\u00ee\3\2\2\2K\u00f1\3\2\2\2M\u00f3\3\2\2\2O\u0108\3\2\2\2Q\u010a\3\2"+
		"\2\2S\u0112\3\2\2\2UV\7]\2\2VW\7_\2\2W\4\3\2\2\2XY\7e\2\2YZ\7n\2\2Z[\7"+
		"c\2\2[\\\7u\2\2\\]\7u\2\2]\6\3\2\2\2^_\7}\2\2_\b\3\2\2\2`a\7\177\2\2a"+
		"\n\3\2\2\2bc\7*\2\2c\f\3\2\2\2de\7+\2\2e\16\3\2\2\2fg\7]\2\2g\20\3\2\2"+
		"\2hi\7_\2\2i\22\3\2\2\2jk\7=\2\2k\24\3\2\2\2lm\7/\2\2mn\7@\2\2n\26\3\2"+
		"\2\2op\t\2\2\2pr\7z\2\2qs\4\62\63\2rq\3\2\2\2st\3\2\2\2tr\3\2\2\2tu\3"+
		"\2\2\2u\30\3\2\2\2vw\7\62\2\2wx\7z\2\2xz\3\2\2\2y{\t\3\2\2zy\3\2\2\2{"+
		"|\3\2\2\2|z\3\2\2\2|}\3\2\2\2}\32\3\2\2\2~\u0080\t\4\2\2\177~\3\2\2\2"+
		"\177\u0080\3\2\2\2\u0080\u0082\3\2\2\2\u0081\u0083\t\5\2\2\u0082\u0081"+
		"\3\2\2\2\u0083\u0084\3\2\2\2\u0084\u0082\3\2\2\2\u0084\u0085\3\2\2\2\u0085"+
		"\u008e\3\2\2\2\u0086\u0088\7.\2\2\u0087\u0089\t\5\2\2\u0088\u0087\3\2"+
		"\2\2\u0089\u008a\3\2\2\2\u008a\u0088\3\2\2\2\u008a\u008b\3\2\2\2\u008b"+
		"\u008d\3\2\2\2\u008c\u0086\3\2\2\2\u008d\u0090\3\2\2\2\u008e\u008c\3\2"+
		"\2\2\u008e\u008f\3\2\2\2\u008f\34\3\2\2\2\u0090\u008e\3\2\2\2\u0091\u0093"+
		"\t\4\2\2\u0092\u0091\3\2\2\2\u0092\u0093\3\2\2\2\u0093\u0095\3\2\2\2\u0094"+
		"\u0096\t\5\2\2\u0095\u0094\3\2\2\2\u0096\u0097\3\2\2\2\u0097\u0095\3\2"+
		"\2\2\u0097\u0098\3\2\2\2\u0098\u0099\3\2\2\2\u0099\u009d\7\60\2\2\u009a"+
		"\u009c\t\5\2\2\u009b\u009a\3\2\2\2\u009c\u009f\3\2\2\2\u009d\u009b\3\2"+
		"\2\2\u009d\u009e\3\2\2\2\u009e\36\3\2\2\2\u009f\u009d\3\2\2\2\u00a0\u00a1"+
		"\7k\2\2\u00a1\u00a2\7h\2\2\u00a2 \3\2\2\2\u00a3\u00a4\7g\2\2\u00a4\u00a5"+
		"\7n\2\2\u00a5\u00a6\7u\2\2\u00a6\u00a7\7g\2\2\u00a7\"\3\2\2\2\u00a8\u00a9"+
		"\5!\21\2\u00a9\u00aa\5\37\20\2\u00aa$\3\2\2\2\u00ab\u00ac\7y\2\2\u00ac"+
		"\u00ad\7j\2\2\u00ad\u00ae\7k\2\2\u00ae\u00af\7n\2\2\u00af\u00b0\7g\2\2"+
		"\u00b0&\3\2\2\2\u00b1\u00b2\7h\2\2\u00b2\u00b3\7q\2\2\u00b3\u00b4\7t\2"+
		"\2\u00b4(\3\2\2\2\u00b5\u00b6\7?\2\2\u00b6*\3\2\2\2\u00b7\u00b8\7>\2\2"+
		"\u00b8,\3\2\2\2\u00b9\u00ba\7>\2\2\u00ba\u00bb\7?\2\2\u00bb.\3\2\2\2\u00bc"+
		"\u00bd\7@\2\2\u00bd\60\3\2\2\2\u00be\u00bf\7@\2\2\u00bf\u00c0\7?\2\2\u00c0"+
		"\62\3\2\2\2\u00c1\u00c5\7$\2\2\u00c2\u00c4\13\2\2\2\u00c3\u00c2\3\2\2"+
		"\2\u00c4\u00c7\3\2\2\2\u00c5\u00c6\3\2\2\2\u00c5\u00c3\3\2\2\2\u00c6\u00c8"+
		"\3\2\2\2\u00c7\u00c5\3\2\2\2\u00c8\u00c9\7$\2\2\u00c9\64\3\2\2\2\u00ca"+
		"\u00cb\7v\2\2\u00cb\u00cc\7t\2\2\u00cc\u00cd\7w\2\2\u00cd\u00ce\7g\2\2"+
		"\u00ce\66\3\2\2\2\u00cf\u00d0\7h\2\2\u00d0\u00d1\7c\2\2\u00d1\u00d2\7"+
		"n\2\2\u00d2\u00d3\7u\2\2\u00d3\u00d4\7g\2\2\u00d48\3\2\2\2\u00d5\u00d6"+
		"\7-\2\2\u00d6:\3\2\2\2\u00d7\u00d8\7/\2\2\u00d8<\3\2\2\2\u00d9\u00da\7"+
		",\2\2\u00da>\3\2\2\2\u00db\u00dc\7\61\2\2\u00dc@\3\2\2\2\u00dd\u00de\7"+
		"u\2\2\u00de\u00df\7g\2\2\u00df\u00e0\7v\2\2\u00e0B\3\2\2\2\u00e1\u00e2"+
		"\7i\2\2\u00e2\u00e3\7g\2\2\u00e3\u00e4\7v\2\2\u00e4D\3\2\2\2\u00e5\u00e6"+
		"\7.\2\2\u00e6F\3\2\2\2\u00e7\u00e8\7t\2\2\u00e8\u00e9\7g\2\2\u00e9\u00ea"+
		"\7v\2\2\u00ea\u00eb\7w\2\2\u00eb\u00ec\7t\2\2\u00ec\u00ed\7p\2\2\u00ed"+
		"H\3\2\2\2\u00ee\u00ef\7\60\2\2\u00ef\u00f0\7\60\2\2\u00f0J\3\2\2\2\u00f1"+
		"\u00f2\7\60\2\2\u00f2L\3\2\2\2\u00f3\u00f4\7a\2\2\u00f4\u00fc\t\6\2\2"+
		"\u00f5\u00fd\7:\2\2\u00f6\u00f7\7\63\2\2\u00f7\u00fd\78\2\2\u00f8\u00f9"+
		"\7\65\2\2\u00f9\u00fd\7\64\2\2\u00fa\u00fb\78\2\2\u00fb\u00fd\7\66\2\2"+
		"\u00fc\u00f5\3\2\2\2\u00fc\u00f6\3\2\2\2\u00fc\u00f8\3\2\2\2\u00fc\u00fa"+
		"\3\2\2\2\u00fdN\3\2\2\2\u00fe\u00ff\7a\2\2\u00ff\u0100\7h\2\2\u0100\u0105"+
		"\3\2\2\2\u0101\u0102\7\65\2\2\u0102\u0106\7\64\2\2\u0103\u0104\78\2\2"+
		"\u0104\u0106\7\66\2\2\u0105\u0101\3\2\2\2\u0105\u0103\3\2\2\2\u0106\u0109"+
		"\3\2\2\2\u0107\u0109\t\7\2\2\u0108\u00fe\3\2\2\2\u0108\u0107\3\2\2\2\u0109"+
		"P\3\2\2\2\u010a\u010e\t\b\2\2\u010b\u010d\t\t\2\2\u010c\u010b\3\2\2\2"+
		"\u010d\u0110\3\2\2\2\u010e\u010c\3\2\2\2\u010e\u010f\3\2\2\2\u010fR\3"+
		"\2\2\2\u0110\u010e\3\2\2\2\u0111\u0113\t\n\2\2\u0112\u0111\3\2\2\2\u0113"+
		"\u0114\3\2\2\2\u0114\u0112\3\2\2\2\u0114\u0115\3\2\2\2\u0115\u0116\3\2"+
		"\2\2\u0116\u0117\b*\2\2\u0117T\3\2\2\2\22\2t|\177\u0084\u008a\u008e\u0092"+
		"\u0097\u009d\u00c5\u00fc\u0105\u0108\u010e\u0114\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}