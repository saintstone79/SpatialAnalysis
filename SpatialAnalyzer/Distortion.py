import cv2
import numpy as np
import math

# Load images
path = "Your Folder"
image_1 = np.zeros((720,960,3))
image_4 = cv2.imread(path+"/Euisuk.png")

# Four points at the edges of frame
Points_1a=np.array([[0,0,1],[0,960,1],[720,0,1],[720,960,1]])
Points_1d=np.array([[173,99,1],[192,682,1],[700,272,1],[417,1057,1]])

# Finding Homography P of Ph=t
def Homography(points_src,points_dest):
    Mat_A=np.zeros((8,8)) # 8x8 array
    t = np.zeros((1,8)) # 1x8 array
    if points_src.shape[0] != points_dest.shape[0] or points_src.shape[1]!= points_dest.shape[1]:
        exit(1)
    for i in range(0,len(points_src)):
        Mat_A[i*2]=[points_src[i][0],   
                    points_src[i][1],
                    points_src[i][2],
                    0,
                    0,
                    0,
                    (-1*points_src[i][0]*points_dest[i][0]),
                    (-1*points_src[i][1]*points_dest[i][0])]

        Mat_A[i*2+1]=[0,
                      0,
                      0,
                      points_src[i][0],
                      points_src[i][1],
                      points_src[i][2],
                      (-1*points_src[i][0]*points_dest[i][1]),
                      (-1*points_src[i][1]*points_dest[i][1])]

        t[0][i*2] = points_dest[i][0]
        t[0][i*2+1] = points_dest[i][1]
        # P, t matrix formed

    # Perpendicular bisectors of the bounding box
    tmp_H=np.dot(np.linalg.pinv(Mat_A), t.T)
    homography= np.zeros((3,3))
    homography[0]= tmp_H[0:3,0]
    homography[1]= tmp_H[3:6,0]
    homography[2][0:2]= tmp_H[6:8,0]
    homography[2][2]= 1
    return homography

# image mapping
def image_mapping(src_image,dest_image,points_src,Homography):
    tmp_srcimage=np.zeros((src_image.shape[0],src_image.shape[1],3),dtype='uint8')
    pts = np.array([[points_src[0][1],points_src[0][0]],[points_src[1][1],points_src[1][0]],[points_src[3][1],points_src[3][0]],[points_src[2][1],points_src[2][0]]])
    cv2.fillPoly(tmp_srcimage,[pts],(255,255,255))
    for i in range(0,(src_image.shape[0]-1)):
        for j in range(0,(src_image.shape[1]-1)):
            if tmp_srcimage[i,j,1]==255 and tmp_srcimage[i,j,0]==255 and tmp_srcimage[i,j,2]==255:
                point_tmp = np.array([i, j, 1])
                trans_coord = np.array(np.dot(Homography,point_tmp))
                trans_coord = trans_coord/trans_coord[2]
                if (trans_coord[0]>0) and (trans_coord[0]< dest_image.shape[0])and (trans_coord[1]>0) and (trans_coord[1]<dest_image.shape[1]):
                    src_image[i][j]=dest_image [math.floor(trans_coord[0]),math.floor(trans_coord[1])]
            else:
                continue
    return src_image
# image mapping code ends here.

# Transforming the image
H=Homography(Points_1a,Points_1d)
output=image_mapping(image_1,image_4,Points_1a,H)
cv2.imwrite('final_myimage1.jpg',output)