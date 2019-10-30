﻿
// File generated by Wsutil Compiler version 1.0098 
// This file defines C/C++ functions, callbacks, types that correspond to operations,
// and types specified in WSDL and XSD files processed by Wsutil compiler. The generated 
// type definitions, function and callback declarations can be used with various 
// Web Services APIs either in applications that send and receive requests to and 
// from a running web service, or in the implementation of web services, or in both. 
//
// If Wsutil has generated this file from an XSD file, the file contains definition of 
// C/C++ structures types that correspond to types defined in the XSD file. 
// For example, if the following XSD complexType is defined in the XSD file
//
//      <xsd:complexType name="AddRequest">
//          <xsd:sequence>
//              <xsd:element minOccurs="0" name="a" type="xsd:int" />
//              <xsd:element minOccurs="0" name="b" type="xsd:int" />
//          </xsd:sequence>
//      </xsd:complexType>
// 
// this file contains the following definitions of the structure 
//      // AddRequest (xsd:complexType)
//      // <AddRequest xmlns='http://tempuri.org'>
//      // WS_STRUCT_DESCRIPTION* = &calculator_xsd.globalTypes.AddRequest
//      struct AddRequest
//      {
//          int a;
//          int b;
//      };
//
// For more information on how to use the C/C++ types generated in this file to read or write elements of XML documents that conform to 
// this XSD, please see the documentation for 
// WsReadType() and WsWriteType() functions.
// 
#if _MSC_VER > 1000 
#pragma once
#endif

#ifdef __cplusplus
extern "C" {
#endif

// The following types were generated:

//     struct ArrayOfstring;
//     struct ArrayOfstring;

// The following header files must be included in this order before this one

// #include <WebServices.h>

////////////////////////////////////////////////
// C structure definitions for generated types
////////////////////////////////////////////////

// typeDescription: schemas_microsoft_com_2003_10_Serialization_Arrays_xsd.globalTypes.ArrayOfstring
typedef struct ArrayOfstring 
{
    unsigned int StringCount;
    _Field_size_(StringCount)WCHAR** String; // optional
} ArrayOfstring;

////////////////////////////////////////////////
// Global web service descriptions.
////////////////////////////////////////////////

typedef struct _schemas_microsoft_com_2003_10_Serialization_Arrays_xsd
{
    struct // globalTypes
    {
        // xml type: ArrayOfstring ("http://schemas.microsoft.com/2003/10/Serialization/Arrays")
        // c type: ArrayOfstring
        // WS_TYPE: WS_STRUCT_TYPE
        // typeDescription: schemas_microsoft_com_2003_10_Serialization_Arrays_xsd.globalTypes.ArrayOfstring
        WS_STRUCT_DESCRIPTION ArrayOfstring;
        
    } globalTypes;
    struct // globalElements
    {
        // xml element: ArrayOfstring ("http://schemas.microsoft.com/2003/10/Serialization/Arrays")
        // c type: ArrayOfstring
        // elementDescription: schemas_microsoft_com_2003_10_Serialization_Arrays_xsd.globalElements.ArrayOfstring
        WS_ELEMENT_DESCRIPTION ArrayOfstring;
        
    } globalElements;
} _schemas_microsoft_com_2003_10_Serialization_Arrays_xsd;

extern const _schemas_microsoft_com_2003_10_Serialization_Arrays_xsd schemas_microsoft_com_2003_10_Serialization_Arrays_xsd;

#ifdef __cplusplus
}
#endif

