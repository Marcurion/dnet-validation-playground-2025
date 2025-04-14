# Rules
- attributes DataAnnotations  ✅ -> DataAnnotationMeeting.cs
- in method ✅ -> SetMeeting.cs & ErrorOrMeeting.cs
- attributes DataAnnotations extension ✅ -> DataAnnotationMeeting.cs
- Fluent Validation with class attribute ✅ -> AttributeMeeting.cs & ValidAttributeMeeting

# Return types
- void -> SetMeeting.cs
- property -> none -> AttributeMeeting.cs
- ErrorOr -> ErrorOrMeeting.cs


# Batch Validation
- Procedural step-by-step
- Procedural list
- Functional
- try-catch Block

# Error Propagation
- just ErrorOr

# Check for Proof of Concept
- Functional Validation chain is broken by exception ✅
- Localization is possible with ErrorOr  ✅
- Exception validation details (faulty member names) are not lost with ErrorOr ❓
- Convert ErrorOr to Controller Result is easy ❓

# Lessons learned
- Order of access becomes very relevant with "Always Valid"