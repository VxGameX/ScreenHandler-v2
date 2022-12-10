# ScreenHandler v.2

Inputs: text, range, checkbox, radiobutton, int, float

Form struct:
{
    "type": "form",
    "id": "mainScreen",
    "title": {
        "label": "Order Request",
        "centralized": true,
        "bold": true,
        "foregroundColor": "white",
        "backgroundColor": "black"
    },
    "sections": [
        {
            "label": "Are you a new or existing customer?",
            "input": {
                "type": "radiobutton",
                "options": [
                    "I am a new customer",
                    "I am an existing customer"
                ]
            },
            "required": false
        },
        {
            "name": "What is the item you would liket to order?",
            "input": {
                    "int",
                    "range": {
                        "min": 1,
                        "max": 10
                    }
                },
            "required": true
        },
        {
            "name": "¿?",
            "type": "int",
            "required": true
        }
    ],
    "actions": [
        {
            "name": "save",
            "option": "G",
            "handler": "Program.Save",
            "succ": "personEntry",
            "fail": "personEntryFailure"
        },
        {
            "name": "cancel",
            "option": "C",
            "handler": "Program.Cancel",
            "succ": "EXIT!",
            "fail": "EXIT!"
        }
    ]
}













Message struct:
{
    "id": "mainScreen",
    "title": {
        "label": "Encuesta Amos de Llaves",
        "centralized": true,
        "color": "white"
    },
    "type": "message",
    "fields": [
        { "name": "¿Cree usted que los hombres deban quedarse en casa?", "type": "string", "required": true },
        { "name": "¿Cree usted que los hombres deban realizar los quehaceres del hogar?", "type": "string", "required": true },
        { "name": "¿?", "type": "int", "required": true }
    ],
    "actions": [
        { "name": "save", "option": "G", "handler": "Program.Save", "succ": "personEntry", "fail": "personEntryFailure" },
        { "name": "cancel", "option": "C", "handler": "Program.Cancel", "succ": "EXIT!", "fail": "EXIT!" }
    ]
}
