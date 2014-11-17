# Bundle: Authorization

The Authorization Bundle extends RavenDB and adds document level permissions. The Auth Bundle allows to define permissions for a specific user, a group, or using arbitrary tagging. The system is design to be of a high performance, scalable, and flexible. 

## Design Guidelines

Unlike other security systems you may be familiar with (such as RDMBS row level security), the Auth Bundle takes a different approach for securing documents. Instead of having a predefined set of generic operations (READ, WRITE, DELETE) that apply per user or per group, the Auth Bundle allows you to define your own operations. One of the key scenarios when designing the Auth Bundle was supporting multi axis permission systems. This means that the Auth Bundle model is much more suited to defining security concerns for business operations, rather than just blanket can do/ can't do approach taken by most other systems.

Consider the following scenario for a chain of medical clinics:

* Any Nurse can schedule appointments for a Patient.
* Only Patients in the same Clinic can be accessed.
* Only a Doctor can prescribe medicine.

What is interesting here, is that the above requirement are both business logic and security requirements. Moreover, we have at least two operations (scheduling an appointment and prescribing medicine) which boil down to a write permission. Obviously, simply assigning READ/WRITE permissions will not do. That is what we mean when we consider the Auth Bundle to be a multi axis permission system.

Operations in the Auth Bundle are user defined, so when a nurse tries to schedule an appointment for a patient, we will use the "appointment/schedule" operation, and the write operation would succeed, yet if the nurse tries to prescribe medicine, the permission for "medicine/prescribe" will fail.

The Auth Bundle performs the following functions:

* For queries, it filters out any documents for which the user doesn't have a current  operation permission.
* For loading a documents by key, it raises an error if the user doesn't have a permission for the current operation.
* For writing/deleting, it raises an error if the user doesn't have a permission for the current operation.

The most important assumption which the Auth Bundle makes is that we can trust the client not to lie about whose user is executing a certain operation. It assumes the following deployment scenario:

![Figure 1: Authorization](images\authorization_bundle_faq.png)

In other words, only the application server can talk to the RavenDB server and the application server is running trusted code. To be clear, this design does not apply if users can connect directly to the database and lie about who they are. 

## Operations

Unlike most security systems, the Auth Bundle isn't limited to a set of predefined operations (Read, Write, Delete) but is actually more focused on the multi permissions axis. An operation on a document may be accessible for a user in one context, and inaccessible in another.

For example, let us consider the following scenario in a medical clinic:

* A Nurse can schedule an appointment to a Patient.
* A Doctor can authorize hospitalization.
* Viewing Patient details is allowed only to users of the Patient's Clinic.

Note that a Nurse is authorized to modify a Patient's document (to schedule an appointment), but only under certain circumstances. In a system that allows only Read, Write, and Delete permissions, you would have to give the Nurse permissions to write to the Patient document and handle the rationale of the different scenarios, using business logic.

The Auth Bundle allows for a much richer permissions system.

We can define the first two requirements in the above scenario using the following:

{CODE authorization1@Server\Bundles\Authorization.cs /}

And the last one is defined using:

{CODE authorization2@Server\Bundles\Authorization.cs /}

Note that we have multiple axis of permissions for the same document.

## Permissions

Permissions are pretty simple, as they allow or deny access to a document for a specific operation. Permissions have the following properties:

* Permissions can be prioritized, so permission at a higher priority will override permission at a lower priority.
* When there is an 'allow permission' and a 'deny permission' at the same priority, the 'deny permission' will override the 'allow permission'.
* If there is no permission that allows the operation, the operation is denied.

## Tagging entities
Entity tagging is one of the ways in which you can provide additional information for the Auth Bundle. Let us look at how the "Hospitalization/Authorize" permission is set up for our patient, Mary.

Mary is tagged as a Patient, so when DrHowser tries to authorize hospitalization for her, the following things happen:

* The set of permissions associated with the user itself is scanned, however, there is nothing  for "Hospitalization/Authorize" there.
* Then the role document is inspected, and we find that there exists a permission for "Hospitalization/Authorize" for the documents tagged with "Patient".
* That tag matches the tag on the patient's document.

Authorization is given and the operation can proceed.

## Users

The Auth Bundle uses the AuthorizationUser document to define a user. Please note that as long as your user document matches or extends the following document's format, you can use your own user document, and not necessarily the one specified here. 

## Hierarchies

Tags, operations, and roles are hierarchical, yet the way they work is quite unusual:

* In case of Tags, the permission on "Clinics" will match the document tagged with "Clinics/Kirya".
* In case of Operations, the permission for "Hospitalization" will match the operation "Hospitalization/Authorize"
* In case of Roles, if you are a member of the "Doctors/Pediatrician", you are also a member of the "Doctors".

## Installation
To install the Auth Bundle on the server side, simply place the Raven.Bundles.Authorization.dll in the Plugins directory.

To use the Auth Bundle on the client side, you need to reference the following:

* Raven.Bundles.Authorization.dll
* Raven.Client.Authorization.dll

and import the "Raven.Client.Authorization" namespace to include the authorization extension methods.

## Applying permissions
The responses for denying an operation are worth noting:

* When performing a query over a set of documents, some of which we don't have the permission for under the specified operation, those documents are filtered out from the query.
* When loading a document by id, when we don't have the permission to do so under the specified operation, an error is raised.
* When trying to write to a document (either PUT or DELETE), when we don't have the permission to do so under the specified operation, an error is raised.

## Results from indexes
While documents queried using an index will be filtered according to the security rules, there isn't any filtering for fetching the data received directly from the index. Fetching data from the index requires an explicit action (marking the field as stored), so it usually is not a concern, yet be aware of the fact that even with the Auth Bundle, if you are storing fields in the indexes, they can be read regardless of the authorization setup you have.

For much the same reason, the results of map/reduce indexes cannot be secured, and will have no filtering applied to them.

## Related articles


* [Client API : How to work with authorization bundle?](../../client-api/bundles/how-to-work-with-authorization-bundle)