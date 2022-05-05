<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <table border="1">
      <tr>
        <th>User Login</th>
        <th>Matter Number</th>
        ...
      </tr>
      <xsl:for-each select="NewDataSet/RecentMatter">
        <tr>
          <td>
            <xsl:value-of select="UserLogin"/>
          </td>
          <td>
            <xsl:value-of select="MatterNumber"/>
          </td>
          ...
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>