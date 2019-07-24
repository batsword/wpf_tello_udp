"""License.

Copyright 2018 PingguSoft <pinggusoft@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

"""
import sys
sys.path.append("C:\Program Files\IronPython 2.7\lib")


import tello

###############################################################################
# constants
###############################################################################
KEY_MASK_UP    = 0x0001
KEY_MASK_DOWN  = 0x0002
KEY_MASK_LEFT  = 0x0004
KEY_MASK_RIGHT = 0x0008
KEY_MASK_W     = 0x0010
KEY_MASK_S     = 0x0020
KEY_MASK_A     = 0x0040
KEY_MASK_D     = 0x0080
KEY_MASK_SPC   = 0x0100
KEY_MASK_1     = 0x0200
KEY_MASK_2     = 0x0400
KEY_MASK_ESC   = 0x8000

RC_VAL_MIN     = 364
RC_VAL_MID     = 1024
RC_VAL_MAX     = 1684

IDX_ROLL       = 0
IDX_PITCH      = 1
IDX_THR        = 2
IDX_YAW        = 3

###############################################################################
# global variables
###############################################################################
mKeyFlags    = 0
mOldKeyFlags = 0
mRCVal       = [1024, 1024, 1024, 1024]
mDrone       = 0

###############################################################################
# functions
###############################################################################


tblKeyFunctions = {
#    key      toggle   mask
    'up'    : (False, KEY_MASK_UP),
    'down'  : (False, KEY_MASK_DOWN),
    'left'  : (False, KEY_MASK_LEFT),
    'right' : (False, KEY_MASK_RIGHT),
    'w'     : (False, KEY_MASK_W),
    's'     : (False, KEY_MASK_S),
    'a'     : (False, KEY_MASK_A),
    'd'     : (False, KEY_MASK_D),
    'esc'   : (False, KEY_MASK_ESC),
    'space' : (True,  KEY_MASK_SPC),
    '1'     : (True,  KEY_MASK_1),
    '2'     : (True,  KEY_MASK_2),
}



###############################################################################
# main
###############################################################################
print 'Tello Controller                      '
print '+------------------------------------+'
print '|  ESC(quit) 1(360) 2(bounce)        |'
print '+------------------------------------+'
print '|                                    |'
print '|      w                   up        |'
print '|  a       d          left    right  |'
print '|      s                  down       |'
print '|                                    |'
print '|         space(takeoff/land)        |'
print '|                                    |'
print '+------------------------------------+'


def initTello():
    global mDrone
    mDrone = tello.Tello()

def destoryTello():
    mDrone.stop()

def flyLeft():
    mRCVal[IDX_ROLL] = RC_VAL_MIN
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])
def flyRight():
    mRCVal[IDX_ROLL] = RC_VAL_MAX
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])
def flyUp():
    mRCVal[IDX_PITCH] = RC_VAL_MAX
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])
def flyDown():
    mRCVal[IDX_PITCH] = RC_VAL_MIN
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])
def flyA():
    mRCVal[IDX_YAW] = RC_VAL_MIN
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def flaW():
    mRCVal[IDX_THR] = RC_VAL_MAX
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def flyD():
    mRCVal[IDX_YAW] = RC_VAL_MAX
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def flyS():
    mRCVal[IDX_THR] = RC_VAL_MIN
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])


def fly360(flag):
    mDrone.setSmartVideoShot(tello.Tello.TELLO_SMART_VIDEO_360, flag)
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def flyBounce(flag):
    mDrone.bounce(flag)
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def flyNormal():
    mRCVal[IDX_PITCH] = RC_VAL_MID
    mRCVal[IDX_THR] = RC_VAL_MID
    mRCVal[IDX_YAW] = RC_VAL_MID
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])
def takeoffex():
    mDrone.takeOff()
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])

def land():
    mDrone.land()
    mDrone.setStickData(0, mRCVal[IDX_ROLL], mRCVal[IDX_PITCH], mRCVal[IDX_THR], mRCVal[IDX_YAW])


