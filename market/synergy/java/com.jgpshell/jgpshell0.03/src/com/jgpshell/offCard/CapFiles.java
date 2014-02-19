//$Id$

/**
 * Author : Moez Ben MBarka Moez
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */


package com.jgpshell.offCard;

import java.util.Vector;
import java.util.jar.JarInputStream;
import java.util.jar.JarFile;
import java.util.jar.JarEntry;
import java.lang.String;
import java.io.InputStream;
import java.lang.Byte;

/**
 * This class implements methods to parse cap files.
 *
 * @author M. MEZIL
 * @author Moez Ben MBarka
 * @version 6.2
 */
public class CapFiles
{
  /**
   *Components buffer Vector(byte[])[128]
   *<br>128 differents components which corresponds to the 128 components of a cap file (some are optional) constitute the 128 buffer elements.
   *<br>The sending to the card is limited by the size. So the sending must be truncated. the vector contains all parts which all corresponds at one sending to the card.
   *<br>The sending corresponds to a byte[].
   *@see #get() get()
   */
  private Vector[] _caps = new Vector[128];

  /**
   *jar path
   */
  private String _jarPath;

  /**
   *max size to send in one time to the card
   */
  private int _maxSize;


  /**
   *Constructor
   *@param jarPath String corresponds to the jar path
   *@see #_jarPath _jarPath
   *@param maxSize int corresponds to the max size to send in one time to the card
   *@see #_maxSize _maxSize
  */
  public CapFiles(String jarPath, int maxSize) throws UserException
  {
    _jarPath = jarPath;
    _maxSize = maxSize;
    try
    {
      set();
      if (_caps[0] == null)
        throw new Exception ("No Header component in the cap file");
      if (_caps[1] == null)
        throw new Exception ("No Directory component in the cap file");
      if (_caps[3] == null)
        throw new Exception ("No Applet component in the cap file");
    }
    catch(Exception e)
    {
      throw new UserException(e.getMessage());
    }
  }


  /**
   *Method which feel a component Vector of the components buffer {@link #_caps _caps}.
   *<br>If the component is not present into the jar, the component buffer element is initialise at null.
   *@param text String correspond to the component
   *@param index int correspond to the ordre of the component in the components buffer _caps
   *@exception e error occurs during the jar file reading
   */
  private void set() throws Exception
  {
    int offset = 0;
    int length = 0;
    byte[] cbuf = new byte[_maxSize];
    Vector vec;
    JarFile jf;
    java.util.Enumeration enume;
    JarEntry je = null;
    InputStream is;
    try
    {
      jf = new JarFile(_jarPath);
      enume = jf.entries();
    }
    catch(Exception e)
    {
      throw new Exception(_jarPath + " Read error");
    }
    while (enume.hasMoreElements())
    {
      vec = new Vector();
      JarEntry jfe = (JarEntry)(enume.nextElement());
      if (jfe.getName().endsWith(".cap"))
      {
        je = jfe;
        is = jf.getInputStream(je);
        length = is.read(cbuf, offset, _maxSize);
        while(length == _maxSize)
        {
          vec.add(cbuf);
          cbuf = new byte[_maxSize];
          length = is.read(cbuf, offset, _maxSize);
        }
        if (length != -1)
        {
          byte[] cbuf2 = new byte[length];
          for (int i = 0 ; i < length ; i++)
            cbuf2[i] = cbuf[i];
          vec.add(cbuf2);
        }
        is.close();
        int index = ((int)(((byte[])(vec.elementAt(0)))[0]));
        switch(index)
        {
          case 3 : _caps[3] = vec;
            break;
          case 4 : _caps[2] = vec;
            break;
          case 5 : _caps[8] = vec;
            break;
          case 6 : _caps[4] = vec;
            break;
          case 7 : _caps[5] = vec;
            break;
          case 8 : _caps[6] = vec;
            break;
          case 9 : _caps[9] = vec;
            break;
          case 10 : _caps[7] = vec;
            break;
          case 11 :
            break;
          default : _caps[index - 1] = vec;
        }
      }
    }
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#get()
 */
  public Vector get()
  {
    Vector vec = new Vector();
    byte[] buf = new byte[_maxSize];
    byte[] bufRead = new byte[_maxSize];
    int index = 0;
    int sizeLast = 0;
    for (int indexComponent = 0 ; indexComponent < _caps.length ; indexComponent ++)
      if (_caps[indexComponent] != null)
        for (int i = 0 ; i < _caps[indexComponent].size() ; i++)
        {
          bufRead = ((byte[])(_caps[indexComponent].elementAt(i)));
          int indexFin = index + bufRead.length;
          for (int j = index ; j < indexFin ; j++)
          {
            buf[j%_maxSize] = bufRead[j-index];
	    sizeLast ++;
            if (j == _maxSize - 1)
            {
              vec.add(buf);
              buf = new byte[_maxSize];
	      sizeLast = 0;
            }
          }
          index = indexFin % _maxSize;
        }
    if (sizeLast != 0)
    {
      byte[] buf2 = new byte[sizeLast];
      for (int i = 0 ; i < sizeLast ; i++)
	buf2[i] = buf[i];
      vec.add(buf2);
    }
    return vec;
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDLengthHeader()
 */
  public byte getAIDLengthHeader()
  {
    return ((byte[])(_caps[0].elementAt((12 - 12 % _maxSize) / _maxSize)))[12 % _maxSize];
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDHeader()
 */
  public byte[] getAIDHeader()
  {
    int taille = getAIDLengthHeader();
    byte[] tab = new byte[taille];
    for (int i = 0 ; i < taille ; i++)
      tab[i] = ((byte[])(_caps[0].elementAt((13 + i - (13 + i) % _maxSize) / _maxSize)))[(13 + i) % _maxSize];
    return tab;
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDLengthDirectory()
 */
  public byte getAIDLengthDirectory()
  {
    return ((byte[])(_caps[1].elementAt((33 - 33 % _maxSize) / _maxSize)))[33 % _maxSize];
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDDirectory()
 */
  public byte[] getAIDDirectory()
  {
    int taille = getAIDLengthDirectory();
    byte[] tab = new byte[taille];
    for (int i = 0 ; i < taille ; i++)
      tab[i] = ((byte[])(_caps[1].elementAt((34 + i - (34 + i) % _maxSize) / _maxSize)))[(34 + i) % _maxSize];
    return tab;
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getNumberOfApplets()
 */
  public int getNumberOfApplets()
  {
    return (int)(((byte[])(_caps[3].elementAt((3 - 3 % _maxSize) / _maxSize)))[3 % _maxSize]);
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDLengthApplet()
 */
  public Vector getAIDLengthApplet()
  {
    int nbApplets = getNumberOfApplets();
    Vector vec = new Vector();
    int index = 4;
    for (int i = 0 ; i < nbApplets ; i++)
    {
      byte b = ((byte[])(_caps[3].elementAt((index - index % _maxSize) / _maxSize)))[index % _maxSize];
      vec.addElement(new Byte(b));
      index += 1 + (int)b + 2;
    }
    return vec;
  }


  /* (non-Javadoc)
 * @see offCard.FauxCapFiles#getAIDApplet()
 */
  public Vector getAIDApplet()
  {
    int nbApplets = getNumberOfApplets();
    Vector vec = new Vector();
    Vector length = getAIDLengthApplet();
    int index = 5;
    for (int i = 0 ; i < nbApplets ; i++)
    {
      int taille = ((Byte)(length.elementAt(i))).intValue();
      byte[] tab = new byte[taille];
      for (int j = 0 ; j < taille ; j++)
        tab[j] = ((byte[])(_caps[3].elementAt((index + j - (index + j) % _maxSize) / _maxSize)))[(index + j) % _maxSize];
      vec.addElement(tab);
      index += 1 + taille + 2;
    }
    return vec;
  }
}


