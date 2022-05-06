<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
      <table border="1">
        <tr>
          <th>User Login</th>
          <th>Matter Number</th>
          ...
        </tr>
        <xsl:for-each select="test-results/test-suite/results">
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
    </html>
  </xsl:template>
</xsl:stylesheet>