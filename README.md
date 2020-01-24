Based on a sample from Dominic Baier
See [this](https://leastprivilege.com/2019/01/18/an-alternative-way-to-secure-spas-with-asp-net-core-openid-connect-oauth-2-0-and-proxykit/) blog post for more details.

Slightly modified to better match my own scenario:
* SPA frontend.
* Private backend, with some additional http calls to crossdomain "microservices".