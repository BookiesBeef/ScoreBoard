
# ScoreBoard
*Library to handle a scoreboard.*

**Functionalities:**

1. Starts a game, adding it to the scoreboard.
2. Finish a game, removing it from the scoreboard.
3. Update score on a scoreboard game.
4. Get a summary of games by total score. Those games with the same total score will be
returned ordered by the most recently added to our system.

**Considerations:**

- Tests are mainly integration ones, covering all service functionalities.
- The repository interface allows to change the persistence method with ease.
- Solution follows SOLID principles with testability and maintenability in mind, as well as source code is structured to create descriptive namespaces.
- Both the models and the repository could have been programmed simpler, even eliminating the repository directly, and the tests would have been much simpler.
