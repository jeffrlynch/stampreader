# stampreader
A small search utility for the StampManage Database
The express purpose of this application is to quickly find stamp details (with pricing) from a command prompt while browsing Ebay to expand my collection. StampManages offers reporting options but in my mind, it seems simpler to just run a command rather than loading the application navigate through multiple areas to issue a report that must be printed or sent to PDF.

For Information:
* StampManage by Liberty Street Software: https://www.libertystreet.com/Stamp-Collecting-Software.htm
* Information about "Scott" Numbers: https://findyourstampsvalue.com/helpful-terms/scott-numbers

##Execution Parameters

Short|Long|Comment
-|-|-
m|mode|Execution mode, **required** can be on of the following: single, multi, missingyear
i|input|blah
y|year|Used in conjunction with the -m missing year option to specify what year to lookup
j|jsonout|Create JSON Output

##Usage

stampreader -m missingyear -y 1975

Will check the StampManage DB to see what stamps are not owned for that year.

