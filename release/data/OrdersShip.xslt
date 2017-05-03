<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <xsl:element name="Orders">  
      <xsl:apply-templates select="/Customers/Customer/Orders/Order"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="/Customers/Customer/Orders/Order">
    <xsl:element name="Order">  
      <xsl:attribute name="ID">
        <xsl:value-of select="@OrderID"/>
      </xsl:attribute>
      <xsl:attribute name="Customer">
        <xsl:value-of select="../../CustomerID  "/>
      </xsl:attribute>
      <xsl:element name="Date">
        <xsl:value-of select="OrderDate"/>
      </xsl:element>
      <xsl:element name="Sum">
        <xsl:value-of select="Freight"/>
      </xsl:element>
      <xsl:element name="Country">
        <xsl:value-of select="ShipCountry"/>
      </xsl:element>
      <xsl:element name="Region">
        <xsl:value-of select="ShipRegion"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
