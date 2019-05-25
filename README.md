# mv-twilio

Tiny console tool to send out SMS to observers (initial scope)
Right now it can be used to 

# Prerequisites:

add a `appsettings.json` file right next to the mv-twilio executable file (if one is not already in there) and fill in the information:

```json
{
  "Template": "",
  "AccountSid": "",
  "AuthToken": "",
  "PhoneNumbers": [ "" ],
  "FailFile": "failed.txt",
  "DefaultFile": "test.txt"
}
````

- `Template` is the message template that will be sent as an SMS
  - you can also specify a `{0}` token to be replaced with the second 
- `AccountSid` and `AuthToken` can be retrieved from your twilio acount
- `PhoneNumbers` are also configured in your twilio account you just fill them in the file. minimum one number is required.
- `FailFile` is the filepath for the output of the failed attempts
- `DefaultFile` is used to get the [input file](/#Input File)
 (if you do not specify one as an argument - see [Usage](#Usage))

# Input File
the file expected is a `txt` file with 2 columns `tab separated`.

the failed attempts will appear in a different file `failed.txt` in the executing folder.

so basically a table. the first column will contain the target phone number (it will receive the message from the template) - the second column will contain the the string 

# Usage
in a console of your choice, navigate to the folder you unpacked this to
```powershell
mv-twilio {path-to-the-file-you-wish-to-parse}
```


ex:
```powershell
mv-twilio "D:\\mv-ro-sms.txt"
```

## Examples

Template: (in appsettings.json)
```cs
Hello, {0}!
```

Input file content:
```cs
+40711222333  dude
+40798765432  Santa Claus
```


End result:

+40711222333 will receive the SMS:
```cs
Hello, dude!
```

+40798765432 will receive the SMS:
```cs
Hello, Santa Claus!
```
