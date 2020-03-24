using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryPuzzle : MonoBehaviour {

    public BookUse[] nonCritBooks;
    public BookUse[] puzzleBooksInOrder;
    bool fail = false;
    int myCurrentPlace = 0;
    public int mySuccessInt;
    LibraryPuzzle me;

    private void Start() {
        me = GetComponent<LibraryPuzzle>();
        if (nonCritBooks.Length != 0) {
            for (int i = 0; i < nonCritBooks.Length; i++) {
                nonCritBooks[i].SetLibPuzzle(me);
            }
        }
        if (puzzleBooksInOrder.Length != 0) {
            for (int i = 0; i < puzzleBooksInOrder.Length; i++) {
                puzzleBooksInOrder[i].SetLibPuzzle(me);
            }
        }
        mySuccessInt = puzzleBooksInOrder.Length;
    }

    public void OnBookUse(BookUse book) {
        if (book != puzzleBooksInOrder[myCurrentPlace]) {
            fail = true;
        }
        myCurrentPlace++;

        if (myCurrentPlace >= mySuccessInt && !fail) {
            //play a 'booooom' sound to signify passing
            //alternatively, use glitch material to make it "fizzle out"
            Destroy(gameObject);
        }
        else if (myCurrentPlace >= mySuccessInt && fail) {
            myCurrentPlace = 0;
            for (int i = 0; i < nonCritBooks.Length; i++) {
                nonCritBooks[i].ResetBooks();
            }
            for (int i = 0; i < puzzleBooksInOrder.Length; i++) {
                puzzleBooksInOrder[i].ResetBooks();
            }
        }
    }
}
