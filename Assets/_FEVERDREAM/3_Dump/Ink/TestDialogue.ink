You enter the living room. There are two photographs on the table: One of your parents, and one of your grandparents.

What will you do?
-> Look_At_Photos_Parents_First

== Look_At_Photos_Parents_First ==
+ [Look at the photo of your parents] -> Parents1
+ [Look at the photo of your grandparents] -> Grandparents

== Look_At_Photos_Grandparents_First ==
+ [Look at the photo of your parents] -> Parents2
+ [Look at the photo of your grandparents] -> Grandparents

== Parents1 ==
They look so happy!
+ [Go back] -> Look_At_Photos_Parents_First

== Parents2 ==
Them too, soon enough.
+ [Go back] -> Look_At_Photos_Grandparents_First

== Grandparents ==
...
+ [Go back] -> Look_At_Photos_Grandparents_First


