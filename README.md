# ScreenHandler v.2

Inputs: text, range, checkbox, radiobutton, int, float

Form struct:
{
  "type": "form",
  "id": "orderRequestForm",
  "title": {
    "label": "Order Request",
    "centralized": true,
    "foregroundColor": "white",
    "backgroundColor": "black"
  },
  "description": "Order requests!",
  "sections": [
    {
      "id": "1",
      "label": "Are you a new or existing customer?",
      "input": {
        "type": "radiobutton",
        "options": ["I am a new customer", "I am an existing customer"]
      },
      "required": true
    },
    {
      "id": "2",
      "label": "What is the item you would like to order?",
      "input": {
        "type": "int"
      },
      "required": true
    },
    {
      "id": "3",
      "label": "What color(s) would you like to order?",
      "input": {
        "type": "checkbox",
        "options": ["color 1", "color 2", "color 3", "color 4"]
      },
      "required": false
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
