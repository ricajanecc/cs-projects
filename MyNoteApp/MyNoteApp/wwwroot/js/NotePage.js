const simplemde = new SimpleMDE({ element: document.getElementById("note") });

async function listView() {
    let listOfNotes = await fetch("/NotePage/ListNote").then(data => data.json()); //brings the list of notes and converts them from a json to an object
    const historyList = document.querySelector(".history-list"); //selects history list 
    historyList.innerHTML = ""; //removes all the previous buttons

    listOfNotes = listOfNotes.sort(function (a, b) //sorts the list of notes by date
    {
        return new Date(b.date) - new Date(a.date);
    });

    for (let i = 0; i < listOfNotes.length; i++) { //goes through the list of notes
        let search = document.querySelector(".searchBox").value;
        if (search !== "" && !listOfNotes[i].title.toUpperCase().includes(search.toUpperCase())) { //if the search is not empty, and if the title does not include the search, it skips printing the button.
            continue;
        }

        const button = document.createElement("button"); //creates a new button, creates a button per note
        button.classList = "button-history"; //changes the class of the buttons to button-history to apply css
        button.innerText = listOfNotes[i].title + " " + new Date(listOfNotes[i].date).toLocaleString(); //changes the content of the button to the title of the note 
        historyList.appendChild(button); //adds the button to the historyList div

        button.onclick = () => {
            document.querySelector("input[name = title]").value = listOfNotes[i].title; //sets the value of input title to the title of the note selector, so the note can be edited
            simplemde.value(listOfNotes[i].text); //changes the editor value to the note that is going to be edited
            document.querySelector("input[name=id]").value = listOfNotes[i].noteId; //changes the id to be the note's id, so that the note can be edited
        }
    }
}

function clearNote() { //resets the title box, text box and the id, so that the user can start a new/blank note
    document.querySelector("input[name = title]").value = "";
    simplemde.value("");
    document.querySelector("input[name=id]").value = "";
}

window.addEventListener("load", () => {  //calls ListView when the page load
    listView();
    document.querySelector(".addButton").onclick = () => {
        clearNote();
    }

    document.querySelector(".searchButton").onclick = () => {
        listView();
    }

    document.querySelector(".saveButton").onclick = () => { //when the user saves, the program waits 1000 ms or 1s and updates the list
        setTimeout(() => {
            listView();
            clearNote();
        }, 1000)
    }

    document.querySelector(".deleteButton").onclick = () => { //when the user deletes, the program waits 1000 ms or 1s and updates the list
        setTimeout(() => {
            listView();
            clearNote();
        }, 1000)
    }
})

