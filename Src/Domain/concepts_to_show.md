# Rules
- attributes DataAnnotations
- in method
- attributes DataAnnotations extension

# Return types
- void
- property -> none
- ErrorOr


# Batch Validation
- Procedural step-by-step
- Procedural list
- Functional
- try-catch Block

# Error Propagation
- just ErrorOr

# Check for Proof of Concept
- Functional Validation chain is broken by exception ✅ 
- Exception validation details (faulty member names) are not lost with ErrorOr ❓
- Convert ErrorOr to Controller Result is easy ❓