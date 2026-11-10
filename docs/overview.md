# Overview

The Bruker SPR Control Software's remote API allows client frameworks to connect to the Control Software remotely and run its most important operations like starting batch jobs or maintenance operations.

The implementation is based on the Microsoft Windows Communication Foundation (WCF) technology which allows to publish and configure several different endpoints to access the API by just changing the Control Software's configuration file. This allows to write clients using a wide range of different languages, connections, and protocols to talk to the API. In order to keep the API as flexible as possible, it does not support callbacks. Updates about the system's status must be obtained using polling.

As the API is specific for the control software it is highly recommended to have a brief understanding of what you can do with the control software, what the SPR machines of Bruker a capable of, and how they (in principle) work. For that it might be helpful to take a look into the manual of the control software[^1].

[^1]: The manual can be found in the control software itself, by clicking on "Description" in the right side bar.
