# The Nexus Programming language

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
