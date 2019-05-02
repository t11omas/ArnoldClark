# ArnoldClark
Technical Test for Arnold Clark

**Assumptions**
No interest rate to keep the problem simple

rounded to 2 decimal places


**Notes**

Add a few sample tests of the loan calculator

Didn't add any client side validation for minimum deposit - this could have been done using JQuery, either manually or added a custom data annotation validation attribute along with client side validation

The arrangement fee and completion fee are configuarble.  The values are stored in a static LoansRules class which can be changed and re run to see the effect this has.  These could have been put into the config or a datastore to allow the values to be changed at runtime to avoid recompiling the application, but given this was a demo app, I went with the simplest option.

I would assume the Delivery date can't be in the past, but there was no mention of that, so i didn't add anything in to guard against that.
