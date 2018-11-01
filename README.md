# stampreader
A small search utility for the StampManage Database
While StampManage is a great solution offered by Liberty Street Software, I just wanted a way to quickly find Stamp information from a command prompt.The express purpose of this application is to quickly find stampe details (with pricing) from a command prompt while browsing Ebay to expand my collection. StampManages offers reporting options but in my mind seems simpler to just run a command rather than loading the application navigate through multiple arias and then issue a report

Execution Parameters:

Short|Long|Comment
-|-|-
m|mode|Execution mode, **required** can be on of the following: single, multi, missingyear
i|input|specify the stamp scotts number(s) mutliple entries are semi-colon delimted i.e.: 201;2401;129
y|year|Used in conjunction with the -m missing year option to specify what year to lookup
j|jsonout|Create JSON Output

Usage:
stampreader -m missingyear -y 1975
Will check the StampManage DB to see what stamps are not owned for that year.
