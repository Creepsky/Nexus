# The Nexus Programming language

[![Managed with Taiga.io](https://img.shields.io/badge/managed%20with-TAIGA.io-709f14.svg)](https://tree.taiga.io/project/creepsky-nexus/ "Managed with Taiga.io") [![Build Status](https://travis-ci.org/Creepsky/Nexus.svg?branch=master)](https://travis-ci.org/Creepsky/Nexus)

A simple yet powerful system programming language.

```c
(int, string) create_tuple() {
    return (123, "abc");
}

auto number, text = create_tuple();

string [] values;
auto upper_bound = 10;

for (auto i : 0..upper_bound) {
    values.add(text);
}

class A {
    long a;
}

int A.extension_function() {
    return a;
}
```

Nexus translates directly to C++ code, which makes interoperabilty with libraries a lot easier, and tries to generate the most performant code, while still being easy to write.

## Main Goals

- write less, do more
- simple and readable code
- high level constructs and syntactic sugar
- built-in asynchronicity through thread pools and channels
- easy to use library manager
- type safety

## How to use the compiler

```
dotnet NexusParser.dll <project dir> <output dir>
```
```
project dir: root directory of your Nexus project
output dir: root directory for your output C++ project
```

## How to compile the Nexus parser

1. Install [.NET core](https://www.microsoft.com/net/download) (tested with 2.1)
2. Install [JDK](https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) (or install it from your OS repository)
2. Download [antlr](http://www.antlr.org/download) (tested with 4.7.1 complete)
3. Run `antlr.sh` on Linux systems or `antlr.bat` on Windows
4. Compile with `dotnet build NexusParser.sln` (or use your favorite IDE)
