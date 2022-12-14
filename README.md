# Screen Handler V.2


### Config. file types
* Form
* Message [^1]

## Form
#### Form struct:
```json
{
  "type": "form",
  "id": "orderRequestForm",
  "title": {
    "label": "Order Request",
    "centralized": true,
    "foregroundColor": "white",
    "backgroundColor": "black"
  },
  "body": {
    "foregroundColor": "white",
    "backgroundColor": "black"
  },
  "description": "After you fill out this oreder request, we will contact you to go over details and availability before the order is completed. If you would like faster service and direct information on current stock and pricing please contact us at (123) 456-7890 or no_reply@example.com",
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
  ]
}
```


##### Form parts
* Description:
    > Required: No
    >> Type: string
    >>> Here you can put a short description of your form, with a max length of 500 characters.

    Example:
    ```json
    {
        ...
        "description": "Here is a short description!"
        ...
    }
    ```

* Sections:
    > Required: Yes
    > Type: Section Array
    > Here you declare your form's sections.
    > Required fields:
        * Id (string)
        * Label (string)
        * Input type (string)

    Example:
    ```json
    {
        ...
        "sections": [
            {
                "id": "section1",
                "label": "This is section 1!",
                "input": {
                    "type:": "radiobutton",
                    "options": ["Cool", "Nah"]
                },
                "required": true
            },
            {
                "id": "section2",
                "label": "This is not section 3 :p",
                "input": {
                    "type:": "text"
                },
                "required": true
            }
        ]
        ...
    }
    ```
    
* Input:
    > Required: Yes
    > Type: Input
    > Here you 
    > Required fields:
        * Type (string) - Values: int, float, text, radiobutton and checkbox
    
    > Contextual fields:
        * Options (string array) Only required when type is checkbox or radiobutton


[^1]: I'm working on it!
