You wake up in your bedroom. You feel terrible. You should probably head to your parents' room to ask for fever medicine. ->Bedroom_Description

=== Bedroom_Description ===
Next to your bed is a mirror. On the other end of the room is the door to the hallway.
-> Bedroom_Choices

=== Bedroom_Choices ===
* [Look in the mirror] -> Mirror
* [Leave your room] -> Hallway

=== Mirror ===
You look just as bad as you feel.
* [Back] -> Bedroom_Description

=== Hallway ===
You step into the hallway. There's an oddly-shaped figure standing in front of your mom and dad's bedroom door. It regards you with casual familiarity.

Strange Being: Ah, you're awake. You want to see your parents, right? -> You_Want_To_See_Your_Parents

=== You_Want_To_See_Your_Parents ===
+ [Get out of my way] -> Hallway_Aggressive

=== Hallway_Aggressive ===
Strange Being: Hey, now. It's not my fault you weren't careful and went and got sick. I'm just protecting your family. 
* [Next] -> Hallway_Cont

=== Hallway_Cont ===
Strange Being: I'll tell you what, if you wash your hands come back here clean, maybe I'll let you by. 
* [Next] -> Hallway_Cont2

=== Hallway_Cont2 ===

Strange Being: But I'm going to watch you. Meticulously. And you're going to listen to me.

-> DONE

