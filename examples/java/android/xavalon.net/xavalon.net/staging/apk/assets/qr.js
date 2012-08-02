// [qr.js](http://neocotic.com/qr.js) 1.0.3  
// (c) 2011 Alasdair Mercer  
// Freely distributable under the MIT license.  
// Based on [jsqrencode](http://code.google.com/p/jsqrencode/)  
// (c) 2010 tz@execpc.com  
// Licensed under the GPL Version 3 license.  
// For all details and documentation:  
// <http://neocotic.com/qr.js>

(function (root) {

  // Private constants
  // -----------------

  var
    // Alignment pattern.
    ALIGNMENT_DELTA = [
      0,  11, 15, 19, 23, 27, 31, 16, 18, 20, 22, 24, 26, 28, 20, 22, 24, 24,
      26, 28, 28, 22, 24, 24, 26, 26, 28, 28, 24, 24, 26, 26, 26, 28, 28, 24,
      26, 26, 26, 28, 28
    ],
    // MIME used to initiate a browser download prompt when `qr.save` is
    // called.
    DOWNLOAD_MIME   = 'image/octet-stream',
    // There are four elements per version. The first two indicate the number
    // of blocks, then the data width, and finally the ECC width.
    ECC_BLOCKS      = [
      1,   0,   19,  7,   1,   0,   16,  10,  1,   0,   13,  13,  1,   0,
      9,   17,  1,   0,   34,  10,  1,   0,   28,  16,  1,   0,   22,  22,
      1,   0,   16,  28,  1,   0,   55,  15,  1,   0,   44,  26,  2,   0,
      17,  18,  2,   0,   13,  22,  1,   0,   80,  20,  2,   0,   32,  18,
      2,   0,   24,  26,  4,   0,   9,   16,  1,   0,   108, 26,  2,   0,
      43,  24,  2,   2,   15,  18,  2,   2,   11,  22,  2,   0,   68,  18,
      4,   0,   27,  16,  4,   0,   19,  24,  4,   0,   15,  28,  2,   0,
      78,  20,  4,   0,   31,  18,  2,   4,   14,  18,  4,   1,   13,  26,
      2,   0,   97,  24,  2,   2,   38,  22,  4,   2,   18,  22,  4,   2,
      14,  26,  2,   0,   116, 30,  3,   2,   36,  22,  4,   4,   16,  20,
      4,   4,   12,  24,  2,   2,   68,  18,  4,   1,   43,  26,  6,   2,
      19,  24,  6,   2,   15,  28,  4,   0,   81,  20,  1,   4,   50,  30,
      4,   4,   22,  28,  3,   8,   12,  24,  2,   2,   92,  24,  6,   2,
      36,  22,  4,   6,   20,  26,  7,   4,   14,  28,  4,   0,   107, 26,
      8,   1,   37,  22,  8,   4,   20,  24,  12,  4,   11,  22,  3,   1,
      115, 30,  4,   5,   40,  24,  11,  5,   16,  20,  11,  5,   12,  24,
      5,   1,   87,  22,  5,   5,   41,  24,  5,   7,   24,  30,  11,  7,
      12,  24,  5,   1,   98,  24,  7,   3,   45,  28,  15,  2,   19,  24,
      3,   13,  15,  30,  1,   5,   107, 28,  10,  1,   46,  28,  1,   15,
      22,  28,  2,   17,  14,  28,  5,   1,   120, 30,  9,   4,   43,  26,
      17,  1,   22,  28,  2,   19,  14,  28,  3,   4,   113, 28,  3,   11,
      44,  26,  17,  4,   21,  26,  9,   16,  13,  26,  3,   5,   107, 28,
      3,   13,  41,  26,  15,  5,   24,  30,  15,  10,  15,  28,  4,   4,
      116, 28,  17,  0,   42,  26,  17,  6,   22,  28,  19,  6,   16,  30,
      2,   7,   111, 28,  17,  0,   46,  28,  7,   16,  24,  30,  34,  0,
      13,  24,  4,   5,   121, 30,  4,   14,  47,  28,  11,  14,  24,  30,
      16,  14,  15,  30,  6,   4,   117, 30,  6,   14,  45,  28,  11,  16,
      24,  30,  30,  2,   16,  30,  8,   4,   106, 26,  8,   13,  47,  28,
      7,   22,  24,  30,  22,  13,  15,  30,  10,  2,   114, 28,  19,  4,
      46,  28,  28,  6,   22,  28,  33,  4,   16,  30,  8,   4,   122, 30,
      22,  3,   45,  28,  8,   26,  23,  30,  12,  28,  15,  30,  3,   10,
      117, 30,  3,   23,  45,  28,  4,   31,  24,  30,  11,  31,  15,  30,
      7,   7,   116, 30,  21,  7,   45,  28,  1,   37,  23,  30,  19,  26,
      15,  30,  5,   10,  115, 30,  19,  10,  47,  28,  15,  25,  24,  30,
      23,  25,  15,  30,  13,  3,   115, 30,  2,   29,  46,  28,  42,  1,
      24,  30,  23,  28,  15,  30,  17,  0,   115, 30,  10,  23,  46,  28,
      10,  35,  24,  30,  19,  35,  15,  30,  17,  1,   115, 30,  14,  21,
      46,  28,  29,  19,  24,  30,  11,  46,  15,  30,  13,  6,   115, 30,
      14,  23,  46,  28,  44,  7,   24,  30,  59,  1,   16,  30,  12,  7,
      121, 30,  12,  26,  47,  28,  39,  14,  24,  30,  22,  41,  15,  30,
      6,   14,  121, 30,  6,   34,  47,  28,  46,  10,  24,  30,  2,   64,
      15,  30,  17,  4,   122, 30,  29,  14,  46,  28,  49,  10,  24,  30,
      24,  46,  15,  30,  4,   18,  122, 30,  13,  32,  46,  28,  48,  14,
      24,  30,  42,  32,  15,  30,  20,  4,   117, 30,  40,  7,   47,  28,
      43,  22,  24,  30,  10,  67,  15,  30,  19,  6,   118, 30,  18,  31,
      47,  28,  34,  34,  24,  30,  20,  61,  15,  30
    ],
    // Final format bits with mask (level << 3 | mask).
    FINAL_FORMAT    = [
      0x77c4, 0x72f3, 0x7daa, 0x789d, 0x662f, 0x6318, 0x6c41, 0x6976, /* L */
      0x5412, 0x5125, 0x5e7c, 0x5b4b, 0x45f9, 0x40ce, 0x4f97, 0x4aa0, /* M */
      0x355f, 0x3068, 0x3f31, 0x3a06, 0x24b4, 0x2183, 0x2eda, 0x2bed, /* Q */
      0x1689, 0x13be, 0x1ce7, 0x19d0, 0x0762, 0x0255, 0x0d0c, 0x083b  /* H */
    ],
    // Galois field exponent table.
    GALOIS_EXPONENT = [
      0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1d, 0x3a, 0x74, 0xe8,
      0xcd, 0x87, 0x13, 0x26, 0x4c, 0x98, 0x2d, 0x5a, 0xb4, 0x75, 0xea, 0xc9,
      0x8f, 0x03, 0x06, 0x0c, 0x18, 0x30, 0x60, 0xc0, 0x9d, 0x27, 0x4e, 0x9c,
      0x25, 0x4a, 0x94, 0x35, 0x6a, 0xd4, 0xb5, 0x77, 0xee, 0xc1, 0x9f, 0x23,
      0x46, 0x8c, 0x05, 0x0a, 0x14, 0x28, 0x50, 0xa0, 0x5d, 0xba, 0x69, 0xd2,
      0xb9, 0x6f, 0xde, 0xa1, 0x5f, 0xbe, 0x61, 0xc2, 0x99, 0x2f, 0x5e, 0xbc,
      0x65, 0xca, 0x89, 0x0f, 0x1e, 0x3c, 0x78, 0xf0, 0xfd, 0xe7, 0xd3, 0xbb,
      0x6b, 0xd6, 0xb1, 0x7f, 0xfe, 0xe1, 0xdf, 0xa3, 0x5b, 0xb6, 0x71, 0xe2,
      0xd9, 0xaf, 0x43, 0x86, 0x11, 0x22, 0x44, 0x88, 0x0d, 0x1a, 0x34, 0x68,
      0xd0, 0xbd, 0x67, 0xce, 0x81, 0x1f, 0x3e, 0x7c, 0xf8, 0xed, 0xc7, 0x93,
      0x3b, 0x76, 0xec, 0xc5, 0x97, 0x33, 0x66, 0xcc, 0x85, 0x17, 0x2e, 0x5c,
      0xb8, 0x6d, 0xda, 0xa9, 0x4f, 0x9e, 0x21, 0x42, 0x84, 0x15, 0x2a, 0x54,
      0xa8, 0x4d, 0x9a, 0x29, 0x52, 0xa4, 0x55, 0xaa, 0x49, 0x92, 0x39, 0x72,
      0xe4, 0xd5, 0xb7, 0x73, 0xe6, 0xd1, 0xbf, 0x63, 0xc6, 0x91, 0x3f, 0x7e,
      0xfc, 0xe5, 0xd7, 0xb3, 0x7b, 0xf6, 0xf1, 0xff, 0xe3, 0xdb, 0xab, 0x4b,
      0x96, 0x31, 0x62, 0xc4, 0x95, 0x37, 0x6e, 0xdc, 0xa5, 0x57, 0xae, 0x41,
      0x82, 0x19, 0x32, 0x64, 0xc8, 0x8d, 0x07, 0x0e, 0x1c, 0x38, 0x70, 0xe0,
      0xdd, 0xa7, 0x53, 0xa6, 0x51, 0xa2, 0x59, 0xb2, 0x79, 0xf2, 0xf9, 0xef,
      0xc3, 0x9b, 0x2b, 0x56, 0xac, 0x45, 0x8a, 0x09, 0x12, 0x24, 0x48, 0x90,
      0x3d, 0x7a, 0xf4, 0xf5, 0xf7, 0xf3, 0xfb, 0xeb, 0xcb, 0x8b, 0x0b, 0x16,
      0x2c, 0x58, 0xb0, 0x7d, 0xfa, 0xe9, 0xcf, 0x83, 0x1b, 0x36, 0x6c, 0xd8,
      0xad, 0x47, 0x8e, 0x00
    ],
    // Galois field log table.
    GALOIS_LOG      = [
      0xff, 0x00, 0x01, 0x19, 0x02, 0x32, 0x1a, 0xc6, 0x03, 0xdf, 0x33, 0xee,
      0x1b, 0x68, 0xc7, 0x4b, 0x04, 0x64, 0xe0, 0x0e, 0x34, 0x8d, 0xef, 0x81,
      0x1c, 0xc1, 0x69, 0xf8, 0xc8, 0x08, 0x4c, 0x71, 0x05, 0x8a, 0x65, 0x2f,
      0xe1, 0x24, 0x0f, 0x21, 0x35, 0x93, 0x8e, 0xda, 0xf0, 0x12, 0x82, 0x45,
      0x1d, 0xb5, 0xc2, 0x7d, 0x6a, 0x27, 0xf9, 0xb9, 0xc9, 0x9a, 0x09, 0x78,
      0x4d, 0xe4, 0x72, 0xa6, 0x06, 0xbf, 0x8b, 0x62, 0x66, 0xdd, 0x30, 0xfd,
      0xe2, 0x98, 0x25, 0xb3, 0x10, 0x91, 0x22, 0x88, 0x36, 0xd0, 0x94, 0xce,
      0x8f, 0x96, 0xdb, 0xbd, 0xf1, 0xd2, 0x13, 0x5c, 0x83, 0x38, 0x46, 0x40,
      0x1e, 0x42, 0xb6, 0xa3, 0xc3, 0x48, 0x7e, 0x6e, 0x6b, 0x3a, 0x28, 0x54,
      0xfa, 0x85, 0xba, 0x3d, 0xca, 0x5e, 0x9b, 0x9f, 0x0a, 0x15, 0x79, 0x2b,
      0x4e, 0xd4, 0xe5, 0xac, 0x73, 0xf3, 0xa7, 0x57, 0x07, 0x70, 0xc0, 0xf7,
      0x8c, 0x80, 0x63, 0x0d, 0x67, 0x4a, 0xde, 0xed, 0x31, 0xc5, 0xfe, 0x18,
      0xe3, 0xa5, 0x99, 0x77, 0x26, 0xb8, 0xb4, 0x7c, 0x11, 0x44, 0x92, 0xd9,
      0x23, 0x20, 0x89, 0x2e, 0x37, 0x3f, 0xd1, 0x5b, 0x95, 0xbc, 0xcf, 0xcd,
      0x90, 0x87, 0x97, 0xb2, 0xdc, 0xfc, 0xbe, 0x61, 0xf2, 0x56, 0xd3, 0xab,
      0x14, 0x2a, 0x5d, 0x9e, 0x84, 0x3c, 0x39, 0x53, 0x47, 0x6d, 0x41, 0xa2,
      0x1f, 0x2d, 0x43, 0xd8, 0xb7, 0x7b, 0xa4, 0x76, 0xc4, 0x17, 0x49, 0xec,
      0x7f, 0x0c, 0x6f, 0xf6, 0x6c, 0xa1, 0x3b, 0x52, 0x29, 0x9d, 0x55, 0xaa,
      0xfb, 0x60, 0x86, 0xb1, 0xbb, 0xcc, 0x3e, 0x5a, 0xcb, 0x59, 0x5f, 0xb0,
      0x9c, 0xa9, 0xa0, 0x51, 0x0b, 0xf5, 0x16, 0xeb, 0x7a, 0x75, 0x2c, 0xd7,
      0x4f, 0xae, 0xd5, 0xe9, 0xe6, 0xe7, 0xad, 0xe8, 0x74, 0xd6, 0xf4, 0xea,
      0xa8, 0x50, 0x58, 0xaf
    ],
    // *Badness* coefficients.
    N1              = 3,
    N2              = 3,
    N3              = 40,
    N4              = 10,
    // Version pattern.
    VERSION_BLOCK   = [ 
      0xc94, 0x5bc, 0xa99, 0x4d3, 0xbf6, 0x762, 0x847, 0x60d, 0x928, 0xb78,
      0x45d, 0xa17, 0x532, 0x9a6, 0x683, 0x8c9, 0x7ec, 0xec4, 0x1e1, 0xfab,
      0x08e, 0xc1a, 0x33f, 0xd75, 0x250, 0x9d5, 0x6f0, 0x8ba, 0x79f, 0xb0b,
      0x42e, 0xa64, 0x541, 0xc69
    ];

  // Private variables
  // -----------------

  var
    // Run lengths for badness.
    badBuffer    = [],
    Canvas,
    dataBlock,
    eccBlock,
    // ECC buffer.
    eccBuffer    = [],
    // ECC level (defaults to **L**).
    eccLevel     = 1,
    // Image buffer.
    frameBuffer  = [],
    // Fixed part of the image.
    frameMask    = [],
    fs,
    Image,
    // Indicates whether or not this script is running in NodeJS.
    inNode       = false,
    polynomial   = [],
    // Save the previous value of the `qr` variable.
    previousQr   = root.qr,
    neccBlock1,
    neccBlock2,
    // Data input buffer.
    stringBuffer = [],
    version,
    width;

  // Private functions
  // -----------------

  // Create a new canvas  using `document.createElement` unless script is
  // running in NodeJS, in which case the node-canvas module is used.
  function createCanvas() {
    return (inNode) ? new Canvas() : root.document.createElement('canvas');
  }

  // Create a new image using `document.createElement` unless script is running
  // in NodeJS, in which case the node-canvas module is used.
  function createImage() {
    return (inNode) ? new Image() : root.document.createElement('img');
  }

  // Return the **last** function in the arguments provided, where possible.
  function findLastFunction() {
    for (var i = arguments.length; i >= 0; --i) {
      if (typeof arguments[i] === 'function') return arguments[i];
    }
  }

  // Safely handle cases where synchronization methodology may vary.  
  // In cases where a callback function was specified it should be used to pass
  // the return value of the function provided or any errors that were thrown
  // during the process. Either the return value of the callback function or
  // the error encountered will be returned here.  
  // Otherwise; errors will be thrown as normal and the return value of the
  // function will simply be returned.  
  // When the function provided is called the specified context will be
  // applied.
  function syncSafe(fn, cb, ctx) {
    try {
      var ret = fn.apply(ctx || this);
      // All went OK, so handle result.
      if (typeof cb === 'function') return cb(null, ret);
      return ret;
    } catch (e) {
      // Something went wrong, so bubble the error.
      if (typeof cb === 'function') return cb(e);
      throw e;
    }
  }

  // Set bit to indicate cell in frame is immutable (symmetric around
  // diagonal).
  function setMask(x, y) {
    var bt;
    if (x > y) {
      bt = x;
      x  = y;
      y  = bt;
    }
    bt   = y;
    bt  *= y;
    bt  += y;
    bt >>= 1;
    bt  += x;
    frameMask[bt] = 1;
  }

  // Enter alignment pattern. Foreground colour to frame, background to mask.
  // Frame will be merged with mask later.
  function addAlignment(x, y) {
    var j;
    frameBuffer[x + width * y] = 1;
    for (j = -2; j < 2; j++) {
      frameBuffer[(x + j)     + width * (y - 2)]     = 1;
      frameBuffer[(x - 2)     + width * (y + j + 1)] = 1;
      frameBuffer[(x + 2)     + width * (y + j)]     = 1;
      frameBuffer[(x + j + 1) + width * (y + 2)]     = 1;
    }
    for (j = 0; j < 2; j++) {
      setMask(x - 1, y + j);
      setMask(x + 1, y - j);
      setMask(x - j, y - 1);
      setMask(x + j, y + 1);
    }
  }

  // Exponentiation mod N.
  function modN(x) {
    while (x >= 255) {
      x -= 255;
      x  = (x >> 8) + (x & 255);
    }
    return x;
  }

  // Calculate and append ECC data to the data block. If block is in the string
  // buffer the indices to buffers are used.
  function appendData(data, dataLength, ecc, eccLength) {
    var fb, i, j;
    for (i = 0; i < eccLength; i++) stringBuffer[ecc + i] = 0;
    for (i = 0; i < dataLength; i++) {
      fb = GALOIS_LOG[stringBuffer[data + i] ^ stringBuffer[ecc]];
      if (fb !== 255) {
        for (j = 1; j < eccLength; j++) {
          stringBuffer[ecc + j - 1] = stringBuffer[ecc + j] ^
              GALOIS_EXPONENT[modN(fb + polynomial[eccLength - j])];
        }
      } else {
        for (j = ecc; j < ecc + eccLength; j++) {
          stringBuffer[j] = stringBuffer[j + 1];
        }
      }
      stringBuffer[ecc + eccLength - 1] = fb === 255 ? 0 :
          GALOIS_EXPONENT[modN(fb + polynomial[0])];
    }
  }

  // Check mask since symmetricals use half.
  function isMasked(x, y) {
    var bt;
    if (x > y) {
      bt = x;
      x  = y;
      y  = bt;
    }
    bt   = y;
    bt  += y * y;
    bt >>= 1;
    bt  += x;
    return frameMask[bt];
  }

  // Apply the selected mask out of the 8 options.
  function applyMask(m) {
    var x, y, r3x, r3y;
    switch (m) {
    case 0:
      for (y = 0; y < width; y++) {
        for (x = 0; x < width; x++) {
          if (!((x + y) & 1) && !isMasked(x, y)) {
            frameBuffer[x + y * width] ^= 1;
          }
        }
      }
      break;
    case 1:
      for (y = 0; y < width; y++) {
        for (x = 0; x < width; x++) {
          if (!(y & 1) && !isMasked(x, y)) frameBuffer[x + y * width] ^= 1;
        }
      }
      break;
    case 2:
      for (y = 0; y < width; y++) {
        for (r3x = 0, x = 0; x < width; x++, r3x++) {
          if (r3x === 3) r3x = 0;
          if (!r3x && !isMasked(x, y)) frameBuffer[x + y * width] ^= 1;
        }
      }
      break;
    case 3:
      for (r3y = 0, y = 0; y < width; y++, r3y++) {
        if (r3y === 3) r3y = 0;
        for (r3x = r3y, x = 0; x < width; x++, r3x++) {
          if (r3x === 3) r3x = 0;
          if (!r3x && !isMasked(x, y)) frameBuffer[x + y * width] ^= 1;
        }
      }
      break;
    case 4:
      for (y = 0; y < width; y++) {
        for (r3x = 0, r3y = ((y >> 1) & 1), x = 0; x < width; x++, r3x++) {
          if (r3x === 3) {
            r3x = 0;
            r3y = !r3y;
          }
          if (!r3y && !isMasked(x, y)) frameBuffer[x + y * width] ^= 1;
        }
      }
      break;
    case 5:
      for (r3y = 0, y = 0; y < width; y++, r3y++) {
        if (r3y === 3) r3y = 0;
        for (r3x = 0, x = 0; x < width; x++, r3x++) {
          if (r3x === 3) r3x = 0;
          if (!((x & y & 1) + !(!r3x | !r3y)) && !isMasked(x, y)) {
            frameBuffer[x + y * width] ^= 1;
          }
        }
      }
      break;
    case 6:
      for (r3y = 0, y = 0; y < width; y++, r3y++) {
        if (r3y === 3) r3y = 0;
        for (r3x = 0, x = 0; x < width; x++, r3x++) {
          if (r3x === 3) r3x = 0;
          if (!(((x & y & 1) + (r3x && (r3x === r3y))) & 1) &&
              !isMasked(x, y)) {
            frameBuffer[x + y * width] ^= 1;
          }
        }
      }
      break;
    case 7:
      for (r3y = 0, y = 0; y < width; y++, r3y++) {
        if (r3y === 3) r3y = 0;
        for (r3x = 0, x = 0; x < width; x++, r3x++) {
          if (r3x === 3) r3x = 0;
          if (!(((r3x && (r3x === r3y)) + ((x + y) & 1)) & 1) &&
              !isMasked(x, y)) {
            frameBuffer[x + y * width] ^= 1;
          }
        }
      }
      break;
    }
  }

  // Using the table for the length of each run, calculate the amount of bad
  // image. Long runs or those that look like finders are called twice; once
  // for X and Y.
  function getBadRuns(length) {
    var
      badRuns = 0,
      i;
    for (i = 0; i <= length; i++) {
      if (badBuffer[i] >= 5) badRuns += N1 + badBuffer[i] - 5;
    }
    // FBFFFBF as in finder.
    for (i = 3; i < length - 1; i += 2) {
      if (badBuffer[i - 2] === badBuffer[i + 2] &&
          badBuffer[i + 2] === badBuffer[i - 1] &&
          badBuffer[i - 1] === badBuffer[i + 1] &&
          badBuffer[i - 1] * 3 === badBuffer[i] &&
          // Background around the foreground pattern? Not part of the specs.
          (badBuffer[i - 3] === 0 || i + 3 > length ||
           badBuffer[i - 3] * 3 >= badBuffer[i] * 4 ||
           badBuffer[i + 3] * 3 >= badBuffer[i] * 4)) {
        badRuns += N3;
      }
    }
    return badRuns;
  }

  // Calculate how bad the masked image is (e.g. blocks, imbalance, runs, or
  // finders).
  function checkBadness() {
    var
      b, b1, big, h, x, y,
      bad   = 0,
      bw    = 0,
      count = 0;
    // Blocks of same colour.
    for (y = 0; y < width - 1; y++) {
      for (x = 0; x < width - 1; x++) {
            // All foreground colour.
        if ((frameBuffer[x + width * y] && frameBuffer[(x + 1) + width * y] &&
             frameBuffer[x + width * (y + 1)] &&
             frameBuffer[(x + 1) + width * (y + 1)]) ||
            // All background colour.
           !(frameBuffer[x + width * y] ||
             frameBuffer[(x + 1) + width * y] ||
             frameBuffer[x + width * (y + 1)] ||
             frameBuffer[(x + 1) + width * (y + 1)])) {
          bad += N2;
        }
      }
    }
    // X runs.
    for (y = 0; y < width; y++) {
      badBuffer[0] = 0;
      for (h = b = x = 0; x < width; x++) {
        if ((b1 = frameBuffer[x + width * y]) === b) {
          badBuffer[h]++;
        } else {
          badBuffer[++h] = 1;
        }
        b = b1;
        bw += b ? 1 : -1;
      }
      bad += getBadRuns(h);
    }
    if (bw < 0) bw = -bw;
    big = bw;
    big += big << 2;
    big <<= 1;
    while (big > width * width) {
      big -= width * width;
      count++;
    }
    bad += count * N4;
    // Y runs.
    for (x = 0; x < width; x++) {
      badBuffer[0] = 0;
      for (h = b = y = 0; y < width; y++) {
        if ((b1 = frameBuffer[x + width * y]) === b) {
          badBuffer[h]++;
        } else {
          badBuffer[++h] = 1;
        }
        b = b1;
      }
      bad += getBadRuns(h);
    }
    return bad;
  }

  // Generate the encoded QR image for the string provided.
  function generateFrame(str) {
    var i, j, k, m, t, v, x, y;
    // Find the smallest version that fits the string.
    t = str.length;
    version = 0;
    do {
      version++;
      k = (eccLevel - 1) * 4 + (version - 1) * 16;
      neccBlock1 = ECC_BLOCKS[k++];
      neccBlock2 = ECC_BLOCKS[k++];
      dataBlock  = ECC_BLOCKS[k++];
      eccBlock   = ECC_BLOCKS[k];
      k = dataBlock * (neccBlock1 + neccBlock2) + neccBlock2 - 3 +
          (version <= 9);
      if (t <= k) break;
    } while (version < 40);
    // FIXME: Ensure that it fits insted of being truncated.
    width = 17 + 4 * version;
    // Allocate, clear and setup data structures.
    v = dataBlock + (dataBlock + eccBlock) * (neccBlock1 + neccBlock2) +
        neccBlock2;
    for (t = 0; t < v; t++) eccBuffer[t] = 0;
    stringBuffer = str.slice(0);
    for (t = 0; t < width * width; t++) frameBuffer[t] = 0;
    for (t = 0; t < (width * (width + 1) + 1) / 2; t++) frameMask[t] = 0;
    // Insert finders: Foreground colour to frame and background to mask.
    for (t = 0; t < 3; t++) {
      k = 0;
      y = 0;
      if (t === 1) k = (width - 7);
      if (t === 2) y = (width - 7);
      frameBuffer[(y + 3) + width * (k + 3)] = 1;
      for (x = 0; x < 6; x++) {
        frameBuffer[(y + x) + width * k] = 1;
        frameBuffer[y + width * (k + x + 1)] = 1;
        frameBuffer[(y + 6) + width * (k + x)] = 1;
        frameBuffer[(y + x + 1) + width * (k + 6)] = 1;
      }
      for (x = 1; x < 5; x++) {
        setMask(y + x, k + 1);
        setMask(y + 1, k + x + 1);
        setMask(y + 5, k + x);
        setMask(y + x + 1, k + 5);
      }
      for (x = 2; x < 4; x++) {
        frameBuffer[(y + x) + width * (k + 2)] = 1;
        frameBuffer[(y + 2) + width * (k + x + 1)] = 1;
        frameBuffer[(y + 4) + width * (k + x)] = 1;
        frameBuffer[(y + x + 1) + width * (k + 4)] = 1;
      }
    }
    // Alignment blocks.
    if (version > 1) {
      t = ALIGNMENT_DELTA[version];
      y = width - 7;
      for (;;) {
        x = width - 7;
        while (x > t - 3) {
          addAlignment(x, y);
          if (x < t) break;
          x -= t;
        }
        if (y <= t + 9) break;
        y -= t;
        addAlignment(6, y);
        addAlignment(y, 6);
      }
    }
    // Single foreground cell.
    frameBuffer[8 + width * (width - 8)] = 1;
    // Timing gap (mask only).
    for (y = 0; y < 7; y++) {
      setMask(7, y);
      setMask(width - 8, y);
      setMask(7, y + width - 7);
    }
    for (x = 0; x < 8; x++) {
      setMask(x, 7);
      setMask(x + width - 8, 7);
      setMask(x, width - 8);
    }
    // Reserve mask, format area.
    for (x = 0; x < 9; x++) setMask(x, 8);
    for (x = 0; x < 8; x++) {
      setMask(x + width - 8, 8);
      setMask(8, x);
    }
    for (y = 0; y < 7; y++) setMask(8, y + width - 7);
    // Timing row/column.
    for (x = 0; x < width - 14; x++) {
      if (x & 1) {
        setMask(8 + x, 6);
        setMask(6, 8 + x);
      } else {
        frameBuffer[(8 + x) + width * 6] = 1;
        frameBuffer[6 + width * (8 + x)] = 1;
      }
    }
    // Version block.
    if (version > 6) {
      t = VERSION_BLOCK[version - 7];
      k = 17;
      for (x = 0; x < 6; x++) {
        for (y = 0; y < 3; y++, k--) {
          if (1 & (k > 11 ? version >> (k - 12) : t >> k)) {
            frameBuffer[(5 - x) + width * (2 - y + width - 11)] = 1;
            frameBuffer[(2 - y + width - 11) + width * (5 - x)] = 1;
          } else {
            setMask(5 - x, 2 - y + width - 11);
            setMask(2 - y + width - 11, 5 - x);
          }
        }
      }
    }
    // Sync mask bits. Only set above for background cells, so now add the
    // foreground.
    for (y = 0; y < width; y++) {
      for (x = 0; x <= y; x++) {
        if (frameBuffer[x + width * y]) setMask(x, y);
      }
    }
    // Convert string to bit stream. 8-bit data to QR-coded 8-bit data
    // (numeric, alphanum, or kanji not supported).
    v = stringBuffer.length;
    // String to array.
    for (i = 0; i < v; i++) eccBuffer[i] = stringBuffer.charCodeAt(i);
    stringBuffer = eccBuffer.slice(0);
    // Calculate max string length.
    x = dataBlock * (neccBlock1 + neccBlock2) + neccBlock2;
    if (v >= x - 2) {
      v = x - 2;
      if (version > 9) v--;
    }
    // Shift and re-pack to insert length prefix.
    i = v;
    if (version > 9) {
      stringBuffer[i + 2] = 0;
      stringBuffer[i + 3] = 0;
      while (i--) {
        t = stringBuffer[i];
        stringBuffer[i + 3] |= 255 & (t << 4);
        stringBuffer[i + 2] = t >> 4;
      }
      stringBuffer[2] |= 255 & (v << 4);
      stringBuffer[1] = v >> 4;
      stringBuffer[0] = 0x40 | (v >> 12);
    } else {
      stringBuffer[i + 1] = 0;
      stringBuffer[i + 2] = 0;
      while (i--) {
        t = stringBuffer[i];
        stringBuffer[i + 2] |= 255 & (t << 4);
        stringBuffer[i + 1] = t >> 4;
      }
      stringBuffer[1] |= 255 & (v << 4);
      stringBuffer[0] = 0x40 | (v >> 4);
    }
    // Fill to end with pad pattern.
    i = v + 3 - (version < 10);
    while (i < x) {
      stringBuffer[i++] = 0xec;
      stringBuffer[i++] = 0x11;
    }
    // Calculate generator polynomial.
    polynomial[0] = 1;
    for (i = 0; i < eccBlock; i++) {
      polynomial[i + 1] = 1;
      for (j = i; j > 0; j--) {
        polynomial[j] = polynomial[j] ? polynomial[j - 1] ^
            GALOIS_EXPONENT[modN(GALOIS_LOG[polynomial[j]] + i)] :
            polynomial[j - 1];
      }
      polynomial[0] = GALOIS_EXPONENT[modN(GALOIS_LOG[polynomial[0]] + i)];
    }
    // Use logs for generator polynomial to save calculation step.
    for (i = 0; i <= eccBlock; i++) polynomial[i] = GALOIS_LOG[polynomial[i]];
    // Append ECC to data buffer.
    k = x;
    y = 0;
    for (i = 0; i < neccBlock1; i++) {
      appendData(y, dataBlock, k, eccBlock);
      y += dataBlock;
      k += eccBlock;
    }
    for (i = 0; i < neccBlock2; i++) {
      appendData(y, dataBlock + 1, k, eccBlock);
      y += dataBlock + 1;
      k += eccBlock;
    }
    // Interleave blocks.
    y = 0;
    for (i = 0; i < dataBlock; i++) {
      for (j = 0; j < neccBlock1; j++) {
        eccBuffer[y++] = stringBuffer[i + j * dataBlock];
      }
      for (j = 0; j < neccBlock2; j++) {
        eccBuffer[y++] = stringBuffer[(neccBlock1 * dataBlock) + i +
            (j * (dataBlock + 1))];
      }
    }
    for (j = 0; j < neccBlock2; j++) {
      eccBuffer[y++] = stringBuffer[(neccBlock1 * dataBlock) + i +
          (j * (dataBlock + 1))];
    }
    for (i = 0; i < eccBlock; i++) {
      for (j = 0; j < neccBlock1 + neccBlock2; j++) {
        eccBuffer[y++] = stringBuffer[x + i + j * eccBlock];
      }
    }
    stringBuffer = eccBuffer;
    // Pack bits into frame avoiding masked area.
    x = y = width - 1;
    k = v = 1;
    // inteleaved data and ECC codes.
    m = (dataBlock + eccBlock) * (neccBlock1 + neccBlock2) + neccBlock2;
    for (i = 0; i < m; i++) {
      t = stringBuffer[i];
      for (j = 0; j < 8; j++, t <<= 1) {
        if (0x80 & t) frameBuffer[x + width * y] = 1;
        // Find next fill position.
        do {
          if (v) {
            x--;
          } else {
            x++;
            if (k) {
              if (y !== 0) {
                y--;
              } else {
                x -= 2;
                k = !k;
                if (x === 6) {
                  x--;
                  y = 9;
                }
              }
            } else {
              if (y !== width - 1) {
                y++;
              } else {
                x -= 2;
                k = !k;
                if (x === 6) {
                  x--;
                  y -= 8;
                }
              }
            }
          }
          v = !v;
        } while (isMasked(x, y));
      }
    }
    // Save pre-mask copy of frame.
    stringBuffer = frameBuffer.slice(0);
    t = 0;
    y = 30000;
    // Using `for` instead of `while` since in original Arduino code if an
    // early mask was *good enough* it wouldn't try for a better one since they
    // get more complex and take longer.
    for (k = 0; k < 8; k++) {
      // Returns foreground-background imbalance.
      applyMask(k);
      x = checkBadness();
      // Is current mask better than previous best?
      if (x < y) {
        y = x;
        t = k;
      }
      // Don't increment `i` to a void redoing mask.
      if (t === 7) break;
      // Reset for next pass.
      frameBuffer = stringBuffer.slice(0);
    }
    // Redo best mask as none were *good enough* (i.e. last wasn't `t`).
    if (t !== k) applyMask(t);
    // Add in final mask/ECC level bytes.
    y = FINAL_FORMAT[t + ((eccLevel - 1) << 3)];
    // Low byte.
    for (k = 0; k < 8; k++, y >>= 1) {
      if (y & 1) {
        frameBuffer[(width - 1 - k) + width * 8] = 1;
        if (k < 6) {
          frameBuffer[8 + width * k] = 1;
        } else {
          frameBuffer[8 + width * (k + 1)] = 1;
        }
      }
    }
    // High byte.
    for (k = 0; k < 7; k++, y >>= 1) {
      if (y & 1) {
        frameBuffer[8 + width * (width - 7 + k)] = 1;
        if (k) {
          frameBuffer[(6 - k) + width * 8] = 1;
        } else {
          frameBuffer[7 + width * 8] = 1;
        }
      }
    }
    // Finally, return the image.
    return frameBuffer;
  }

  // qr.js setup
  // -----------

  // Build the publicly exposed API.
  var qr = {

    // Constants
    // ---------

    // Current version of `qr`.
    VERSION: '1.0.3',

    // QR functions
    // ------------

    // Generate the QR code using the data provided and render it on to a
    // `<canvas>` element.  
    // If no `<canvas>` element is specified in the argument provided a new one
    // will be created and used.  
    // ECC (error correction capacity) determines how many intential errors are
    // contained in the QR code.  
    // Optionally, a callback function can be provided which will be called
    // with the `<canvas>` element as the second argument. If an error occurs
    // it will be passed as the first argument to this function, otherwise this
    // argument will be `null`.
    canvas: function (data, callback) {
      callback = findLastFunction(data, callback);
      return syncSafe(function A() {
        switch (typeof data) {
        case 'object': break;
        case 'string':
          data = {value: data};
          break;
        default:
          data = {};
          break;
        }
        var
          cvs, c2d, i, j, px, qf,
          size  = 4,
          steps = 25;
        if (data.size >= 1 && data.size <= 10) size = data.size;
        size *= steps;
        cvs = data.canvas || createCanvas();
        c2d = cvs.getContext('2d');
        c2d.canvas.width = size;
        c2d.canvas.height = size;
        c2d.fillStyle = data.background || '#fff';
        c2d.fillRect(0, 0, size, size);
        if (data.level) {
          switch (data.level.toUpperCase()) {
          case 'L':
            eccLevel = 1;
            break;
          case 'M':
            eccLevel = 2;
            break;
          case 'Q':
            eccLevel = 3;
            break;
          case 'H':
            eccLevel = 4;
            break;
          }
        }
        qf = generateFrame(data.value || '');
        c2d.lineWidth = 1;
        px = size;
        px /= width;
        px = Math.round(px - 0.5);
        c2d.clearRect(0, 0, size, size);
        c2d.fillStyle = data.background || '#fff';
        c2d.fillRect(0, 0, px * (width + 8), px * (width + 8));
        c2d.fillStyle = data.foreground || '#000';
        for (i = 0; i < width; i++) {
          for (j = 0; j < width; j++) {
            if (qf[j * width + i]) c2d.fillRect(px * i, px * j, px, px);
          }
        }
        return cvs;
      }, callback, this);
    },

    // Generate the QR code using the data provided and render it on to a
    // `<img>` element.  
    // If no `<img>` element is specified in the argument provided a new one
    // will be created and used.  
    // ECC (error correction capacity) determines how many intential errors are
    // contained in the QR code.  
    // Optionally, a callback function can be provided which will be called
    // with the `<img>` element as the second argument. If an error occurs it
    // will be passed as the first argument to this function, otherwise this
    // argument will be `null`.
    image: function (data, callback) {
      callback = findLastFunction(data, callback);
      return syncSafe(function B() {
        switch (typeof data) {
        case 'object': break;
        case 'string':
          data = {value: data};
          break;
        default:
          data = {};
          break;
        }
        var
          cvs = this.canvas(data),
          img = data.image || createImage();
        img.src    = cvs.toDataURL();
        img.height = cvs.height;
        img.width  = cvs.width;
        return img;
      }, callback, this);
    },

    // Generate the QR code using the data provided and render it on to a
    // `<canvas>` element and save it as an image file.  
    // If no `<canvas>` element is specified in the argument provided a new one
    // will be created and used.  
    // ECC (error correction capacity) determines how many intential errors are
    // contained in the QR code.  
    // If called in a browser the `path` property/argument is ignored and will
    // simply prompt the user to choose a location and file name. However, if
    // called within NodeJS the file will be saved to specified path.
    // Optionally, a callback function can be provided which will be called
    // once the saving process has started. If an error occurs it will be
    // passed as the first argument to this function, otherwise this argument
    // will be `null`.
    save: function (data, path, callback) {
      callback = findLastFunction(data, path, callback);
      return syncSafe(function C() {
        switch (typeof data) {
        case 'object':
          if (typeof path === 'string' && !data.path) data.path = path;
          break;
        case 'string':
          data = {value: data};
          if (typeof path === 'string') data.path = path;
          break;
        default:
          data = {};
          break;
        }
        var cvs = this.canvas(data);
        if (inNode) {
          // Running in NodeJS so path is required.
          if (typeof data.path !== 'string') {
            throw new TypeError('Invalid path type: ' + typeof data.path);
          }
          var fd, buff, fdAndBuff = function C1() {
            fs.write(fd, buff, 0, buff.length, 0, function C1A(err) {
              fs.close(fd);
              if (err) throw err;
            });
          };
          cvs.toBuffer(function C2(err, _buff) {
            if (err) throw err;
            buff = _buff;
            if (fd) fdAndBuff();
          });
          fs.open(data.path, 'w', 0666, function C3(err, _fd) {
            if (err) throw err;
            fd = _fd;
            if (buff) fdAndBuff();
          });
        } else {
          // Force the image to be downloaded.
          root.location.href = cvs.toDataURL().replace('image/png',
              DOWNLOAD_MIME);
        }
      }, callback, this);
    },

    // Generate the QR code using the data provided and render it on to a
    // `<canvas>` element before returning its data URI.  
    // If no `<canvas>` element is specified in the argument provided a new one
    // will be created and used.  
    // ECC (error correction capacity) determines how many intential errors are
    // contained in the QR code.  
    // Optionally, a callback function can be provided which will be called
    // with the data URI string as the second argument. If an error occurs it
    // will be passed as the first argument to this function, otherwise this
    // argument will be `null`.
    toDataURL: function (data, callback) {
      callback = findLastFunction(data, callback);
      return syncSafe(function D() {
        return this.canvas(data).toDataURL();
      }, callback, this);
    },

    // Utility functions
    // -----------------

    // Run qr.js in *noConflict* mode, returning the `qr` variable to its
    // previous owner.  
    // Returns a reference to `qr`.  
    // Optionally, a callback function can be provided which will be called
    // after the ownership has been restored. If an error occurs it will be
    // passed as the first argument to this function, otherwise this argument
    // will be `null`.
    noConflict: function (callback) {
      return syncSafe(function E() {
        root.qr = previousQr;
        return this;
      }, callback, this);
    }

  };

  // Support
  // -------

  // Export `qr` for NodeJS and CommonJS.
  if (typeof exports !== 'undefined') {
    inNode = true;
    if (typeof module !== 'undefined' && module.exports) {
      exports = module.exports = qr;
    }
    exports.qr = qr;
    // Import required NodeJS modules.
    Canvas = require('canvas');
    Image = Canvas.Image;
    fs = require('fs');
  } else if (typeof define === 'function' && define.amd) {
    define('qr', function Z() {
      return qr;
    });
  } else {
    // In non-HTML5 browser so strip base functionality.
    if (!root.HTMLCanvasElement) {
      qr.canvas    = function () {};
      qr.image     = function () {};
      qr.save      = function () {};
      qr.toDataURL = function () {};
    }
    root.qr = qr;
  }

}(this));