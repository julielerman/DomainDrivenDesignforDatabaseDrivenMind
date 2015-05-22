# DomainDrivenDesignforDatabaseDrivenMind
Deck &amp; Demos from MSIgnite 2015 &amp; Techorama BE conferences

Demos and PowerPoints for Julie Lerman's "Domain-Driven Design for the Database Driven Mind" presentation.
This talk was given at MS Ignite(May 7, 2015), Techorama (May 2015) and DevSum15 (May 2015).

The talk was recorded at MS Ignite and can be viewed on Channel 9. http://channel9.msdn.com/Events/Ignite/2015/BRK3724

The demo is a subset of the full sample application created by Steve Smith (@ardalis) and myself for our "Domain Driven Design Fundamentals" course on Pluralsight.com (bit.ly/PS-DDD).

The Pluralsight course sample includes the front end web app of the application and is a full working demo.

This version does not include the project for the UI but it does include some extra code.
1) A project with the tests that were uses for this presentation.
2) some additional classes and an extra DbContext to demonstrate the use of a domain model plus a persistence model. The extra classes are in the AppointmentScheduling/Infrastructure/AppointmentSchedule.Data project. In there check the "Persistence Model" folder for the classes used as a persistence model classes and also the SchedulingPersistenceContext class which wraps these classes into a DbContext.

The full working sample for the Pluralsight course is available as a download for Pro Subscribers. The course goes into detail about the full sample, while I only focused on particular bits of it for the DDD for the DB Driven Mind presentation.

Enjoy!


Julie Lerman
thedatafarm.com
@julielerman
