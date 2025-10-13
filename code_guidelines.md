# Coding Guidelines

These guidelines prioritize clean code: simple, readable, modern, and minimalistic. Prefer clarity over cleverness.

## Core principles
- Small, cohesive units. Short methods, focused classes, single responsibility.
- Keep it simple (KISS). Prefer the simplest thing that works well.
- You aren’t gonna need it (YAGNI). Avoid speculative abstractions and configuration.
- Prefer composition over inheritance. Apply SOLID pragmatically, not dogmatically.
- Optimize for readability first; performance second (measure before optimizing).
- Fewer moving parts. Delete code before adding code. Prefer defaults and conventions.
- Express intent. Name things well; avoid magic values; prefer explicitness.
- Consistent style. The entire codebase should feel like it was written by one person.

## Project & source layout
- One `class`/`record` per file. Keep files short and focused.
- Favor top-level `Program` and `file-scoped namespace` declarations.
- Group by feature, not type, when it improves discoverability. Avoid deep folder trees.
- Keep public surface area small. Make types and members `internal` unless needed.

## C#/.NET style (modern and minimal)
- Target latest language features allowed by the project.
- Use `var` when the type is obvious, explicit type when it aids readability.
- Prefer `record` for immutable data models; use `required` members when appropriate.
- Use primary constructors for simple data holders.
- Use collection expressions (`[]`) and target-typed `new()` where it improves clarity.
- Prefer expression-bodied members for trivial members; otherwise keep curly braces.

## Naming
- Names communicate intent, not implementation. Use domain language.
- Methods: verbs (`CalculateSlope`), classes: nouns (`SwingAnalyzer`).
- Booleans read as predicates (`isEnabled`, `hasData`).
- Avoid abbreviations and prefixes. Be precise and consistent.

## Nullability & immutability
- We use non-nullable references by default. There is no need to validate whether an object is null.
- NEVER validate inputs which have already been validated elsewhere.
- Prefer immutable types (`record`, `init` setters). Mutate state narrowly and explicitly.
- Use `IReadOnlyList<T>`/`IReadOnlyDictionary<TKey,TValue>` for read-only exposures.

## Error handling
- Use exceptions for exceptional cases; don’t use exceptions for flow control.
- Catch only when you can add value (handle, translate, or add context). Re-throw preserving stack.
- Only use try/catch if it makes sense.

## LINQ and collections
- Prefer clear and direct code over dense query chains.
- Avoid unnecessary allocations (`ToList()`, `ToArray()`) unless required.

## Performance
- Make it right, then fast. Profile before optimizing. Add benchmarks for critical paths.
- Avoid unnecessary allocations and boxing. Use spans/memory types only when justified and readable.
- Prefer value tasks where appropriate for high-frequency APIs.

## APIs & DTOs
- Keep DTOs minimal and explicit. Validate at the edges.
- Prefer pure functions. Avoid hidden side effects.
- Return precise types. Avoid leaking internals.

## Other
- Prefer `async`/`await`. Do not block on async (`.Result`, `.Wait()`), avoid deadlocks.
- The code should be self-documenting. There should be minimal comments.
