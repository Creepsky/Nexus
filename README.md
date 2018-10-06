# The Nexus Programming language

A simple yet powerful system programming language.

```
(i32, string) create_tuple() {
    return (123, "abc");
}

auto number, text = create_tuple();

string [] values;

for (auto i : [0 .. 10]) {
    values.add(text);
}
```

Nexus translates directly to C++ code, which makes interoperabilty with libraries a lot easier, and tries to generate the most performant code, while still being easy to write.

Let Nexus make the annoying stuff for you and concentrate on your work :)

## Main Goals

- write less, do more
- simple and readable code
- high level constructs and syntactic suggar
- built-in asynchronicity through thread pools and channels
- easy to use library manager
- type safety
