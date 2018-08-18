
1.What are ValidationExtensions?
====================================

ValidationsExtensions are a set of method added to Microsoft's X509Certificate2 class which allow you to validate certificates without worry about the ins and outs of this process.

2.Requirements
===============

ValidationExtensions requires BouncyCastle for .NET 1.7 or above. The binary folder contains this dependency.

3.Assembly
===========

The assemble compiled and ready to run is in ValidationExtensions\bin\Release folder.

4.Usage
=======

If you have a X509Certificate2 object, automagically it would have been added the following methods:

* ValidationResponse ValidateExpiryDate()
* ValidationResponse ValidateWithCRL()
* ValidationResponse ValidateWithCRL(String urlCRL)
* ValidationResponse ValidateWithOCSP(X509Certificate2 issuer)
* ValidationResponse ValidateWithOCSP(X509Certificate2 issuer, String urlOCSP)

In all cases, a ValidationResponse object is returned. To know the status of the certificate, use the attribute *status*:

ValidationResponse.status

which returns an object whose type is ValidationExtensions.Enums.CertificateStatus

4.1 ValidateExpiryDate
----------------------

Validates the validity of the X509Certificate2 object. This method do not performs further validations, so use any of the following methods in case this method returns VALID.

4.2 ValidateWithCRL
-------------------

Validate the certificate using a CRL. If no parameter is indicated, ValidationExtensions will search the url in the CRL distribution points extension.
This method will returns a ValidationResponse object with the status and the revocation date if the certificate is revoked.

4.3 ValidateWithOCSP
--------------------

Validate the certificate against a OCSP server. If the url of the ocsp server is not given, ValidationExtensions will search it in the authority authority access extension.

Unlike ValidateWithCRL method, ValidateWithOCSP DOES NOT return the revocation date.

5.Validate certificates without having an X509Certificate2 object
==================================================================

If you want to validate a certificate but, for any unknown reason you have not an X509Certificate2 object (weird I know), you should use the static method ValidateCertificate from the class Validator.

ValidateCertificate(string serialNumber, X509Certificate2 issuer, string urlOCSP)

This method validates a serial number against a given ocsp server.

6.Why not an unique Validation method?
=======================================

Good question. The root of the problem comes from diversity of certificates. Some have the CRL distribution points extension, others OCSP, others both and others no one. Because create an unique method will make the developers life easier at first sight, it could become a hell when a certificate cannot be validated, adding workarounds to you code. However, this extensions are enough high level but primitive at the same time to develop a method for which validate the certificates you expect to work with should be piece of cake.
