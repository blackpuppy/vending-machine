# vending-machine
A simple Vending Machine.

## Run locally
1. Open `Acme.VendingMachine.sln` with Visual Studio 2017 or above.
2. Press `Ctrl` + `F5` to build and run it with IIS Express.
3. If prompted for HTTPS warning, accept it.

## Further work
- improvement
  - move input buffer from server-side to client-side, can dramtically improve user experience
  - use view model to deal with UI related, take responsibility from Machine model
  - plan to use Bll/Dal, but main logic in model.  Clean up and simplify.
  - count cash
- bugs
  - selected product not cleared after purchase
- unit test
- local dev using Docker for Windows
