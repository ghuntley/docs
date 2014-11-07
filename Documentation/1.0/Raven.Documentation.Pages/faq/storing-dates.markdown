#Storing dates in RavenDB

##How should I store dates in RavenDB? Using UTC? Using Local Time?

RavenDB will store and retrieve dates as you give them to it. It doesn't do any sort of time zone calculations, and therefore doesn't really care whatever the dates that you give it are in UTC or Local Time.

The decision whatever to use UTC or Local Time is an application decision, not an infrastructure decision.

**Example 1**

Imagine we are building fancy ASP.NET MVC application with a lot of AJAX code calling some REST JSON controllers. We probably want to deal with Dates on client side to. Remember JSON date is not parsed automatically by the browser itself.

    [ActionName("Rest Person")]
    [HttpPost]
    public JsonResult Create(PersonCreateInputModel model)
    {
    	if (ModelState.IsValid)
    	{
    		var session = MvcApplication.RavenSession;
    		var person = new Person(
    			model.name,
    			model.surname,
    			DateTime.UtcNow
    		);
    		session.Store(person);
    		session.SaveChanges();
    		return Json(person);
    	}
    	Response.StatusCode = (int)HttpStatusCode.BadRequest;
    	return null;
    }

What we see is the controller method creating a new person with name, surname, and created DateTime. We chose DateTime.UtcNow because it's easily parseable by this JavaScript helper method:

    // parse date in this format: "/Date(1198908717056)/"
    var parseNetDate = function (date) {
    	return new Date(Number(date.match(/\d+/)[0]));
    };

[http://stackoverflow.com/questions/62151/datetime-now-vs-datetime-utcnow](http://stackoverflow.com/questions/62151/datetime-now-vs-datetime-utcnow)
