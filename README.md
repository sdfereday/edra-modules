# Edra Modules

A series of modules that can be used in the creation of a 2D adventure game with Unity. Most of them are of my own implementation with numerous research done here and there. If you can make use of them please do, I'll be working on them when I can.


# The Modules

Currently I'm in the process of splitting out the modules so they don't have any depenencies (bar some of them). For instance, a lot of them make use of the Helpers namespace. My top priority is to make them as decoupled as possible right now.

## Animator

#### Animator Setup (in progress)

Prefab >
 - Attach a SpriteRenderer (Unity)
 - Attach a SpriteAnimator (RedPanda.Animator)
 - Attach an AnimatorLogicManager (RedPanda.Animator)

Dependencies >
- Helper scripts (RedPanda.Helpers)

Notes:
Animator expects a list of logic gates. At the point of creation I was using a data loader and parsing from JSON directly on the class. This has since been changed and now expects the right data to have been provided. As of yet I haven't created an intermediary function to handle this (this is in progress however).

## Dialogue
TODO.

## Dungeon
TODO.

## Interaction
TODO.

## Inventory
TODO.

## Storage
TODO.

## UserInput
TODO.

## Toolbox
TODO.
