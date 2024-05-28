
#speaker : Edward
Hello Warrior, welcome to the real world, ready to fight the goblins?

* [Tell me more about the goblins]  -> InformationQuestion
* [Yes] ->Exit


== InformationQuestion == 
What information do you seek?

* [Where are they?]
  You can find them all over the damn place! But most of their settlements are inside the deep caves.
  -> Caves

* [No further questions] Is that all? 
  ->Exit

== Caves == 
*[Deep Caves?] 
    Yes, they were used mostly for mining in the old days, at least before the goblins invaded our lands
    -> Caves
*[Where are these Caves]
    You can't miss them. Just head East and you'll find them!
    -> Caves
    
* [Back to Information] 
   -> InformationQuestion


== Exit ==
And when you have successfully defeated the goblins, make sure to come back to the village!
Good Luck!
-> END
