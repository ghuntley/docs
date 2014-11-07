##Document Key Generation Options
Raven supports the following document key generation strategies:

##Let Raven generate the document key
When the document has no specified key, Raven will generate a new key for the document. Raven uses a sequential guid approach for generating the keys. Sequential guids are still globally unique but they have the advantage that they sort well for indexing.
This is commonly used when you don't care for the document key, such as when saving log entries, or when the user will never be exposed to the document key. 

##Assign a key yourself
You can also decide on the document key before saving it, in which case Raven will use the assigned key.
Common cases for this are when you save documents which already have native id, such as users, in which case you can use the key: "users/ayende"

##Identity key
The final option for key generation in Raven is to ask the database to define REST like keys, such as "posts/2392", Raven supports such keys natively. If you save a document whose key ends with '/', Raven will automatically start tracking identity numbers for the prefix if it doesn't exist and append the current identity value to the key.
This approach is recommended for most scenarios, since it produces keys that are human readable. 