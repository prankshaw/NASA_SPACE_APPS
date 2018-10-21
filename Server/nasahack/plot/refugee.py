import pandas as pd
import numpy as np
import json

g1=pd.read_csv('plot/refugees.csv')
g2=g1.iloc[:,1:]
longi=[]
lati=[]
jsonvalue=[]
keys=[]
g1dict={}
#print(g1.columns)
df = pd.DataFrame()
df.fillna(365)
df['col'] = g1.columns

#print(df)
longi = g1["longitude"].tolist()
lati = g1["latitude"].tolist()

def smallvalues(lat,lon):
    small=0
    reqd=lati[0]
    for i in range(0,206):
        small=abs(lati[i]-lat)
        if small<reqd:
            reqd=small
            index=i
    return reqdvalues(index)
def reqdvalues(index):
        keys=g2.head(0)
        keys=keys.columns.tolist()
        jsonvalue=g2.loc[g2['Country Name']==g2.loc[index, 'Country Name']]
        json = jsonvalue.to_json(orient='split')
        return(json)
        #print(json)
    
      
#print(smallvalues(7.009,32.0987))


    
        
    
               

