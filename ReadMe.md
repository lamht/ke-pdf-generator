# Pdf generator

The pdf generator project

## Features

- Gen pdf from html.
- Support header, footer, background image.
- Support load css, javascript

Request POST {host}/pdf
```sh
{
    "Header":"<!doctype html><html lang='en'> <head> <link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'> </head> <body> <h1 class='text-primary'>Header</h1> </body></html>",
    "Pages": [
        "<h2>Content page 1</h2>",
        "<h2>Content page 2</h2>"
    ],
    "Footer": "<h2>Footer</h2>",    
    "BackgroundUrl": "https://storage.googleapis.com/kikkervndevbucket/pdf2image/59e525ff-2bef-4c87-9b5f-a2a904fc9123/1081445c-e0fe-467c-8a95-427a3db878c5.png",
    "Margins": {
        "Top": 70,
        "Bottom": 20,
        "Left": 20,
        "Right": 20
    }
}
```

