# Verification Pin Reference

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

Verification pins identify the reviewed test sources and exact bytes that CI accepts.
They prevent a changed fixture from silently changing the meaning of a passing test.
Application setup does not require copying or entering these values.

This is a dated inspection snapshot, not a list of currently deployed pins.
The date and table come from the site's shared reference catalog. Each source
link identifies the exact version-controlled configuration or workflow inspected,
so later changes cannot silently alter the evidence behind the recorded value.
The table covers Python and Unity fixture configurations and Python/ROS/MATLAB
receipt workflow checkouts. It is not an inventory of every workflow action pin.
Receiver versions may advance independently and need not select the same source
revision. The owning repository's reviewed configuration remains authoritative.
Deploying a newer Core verifier does not change an older receiver's checkout;
update and validate those receiver pins separately before claiming alignment.

[!include[](../generated/repository-links/verification-pins.md)]

The separately inspected [ROS fixture configuration](xref:repo.python-ros.ci-config)
and [ROS receipt workflow](xref:repo.python-ros.receipt-workflow) use the recorded
Python fixture and receipt identities at this snapshot. They remain independently
versioned sources and require their own reviewed updates.

## Updating the reference

After a receiver update is reviewed, inspect its configuration and update the
catalog's value, immutable source link and snapshot date together. Rebuild the
site to propagate that documentation change. Changing a value here never changes
an executable workflow pin, candidate approval, dependency gitlink or checksum.
Those remain independently reviewed in their owning repositories.

For a new shared fixture, validate the source revision and digest in each consuming
receiver before updating this snapshot. For a new Core acceptance contract, deploy
the reviewed Core verifier and the corresponding receiver workflow/tooling updates
before describing them as aligned. A new Core main commit alone does not update
Python, ROS or MATLAB receipt checkouts.

Release candidates have their own commit, producer run, artifact and archive
checksum. Historical bootstrap and diagnostic tuples are separate from these pins;
their authoritative provenance remains in the owning candidate manifests and
workflow evidence. An old diagnostic result cannot authorize a new promotion.
