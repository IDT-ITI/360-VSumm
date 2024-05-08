from math import sqrt



def group_salient_regions(input_list,frame_number,saliency_path,spatial_distance,fill_loss,resolution):
    # Initialize a dictionary to store the groups based on the x-axis value (first element of each tuple)
    groups_dict = {}
    groups_frame = {}

    groups_scores = {}
    # Iterate through the input list and group elements based on x-axis proximity
    for i,sublist in enumerate(input_list):
        for j,item in enumerate(sublist):
            #print("new item",item)
            added_to_group = False
            lower_distance = 10000
            lower_distance1 = 10000
            b = 10000
            group = 0

            for group_key in groups_dict.keys():

                last_group_items = groups_dict[group_key][-1:]

                for group_item in last_group_items:

                    a = sqrt((((item[0]+item[2]/2) - (group_item[0]+group_item[2]/2))**2) + (((item[1]+item[3]/2)-(group_item[1]+group_item[3]/2))**2)) # give a weight factor for x-axis and y-axis

                    if item[0]-400<0 or resolution[1]-item[0]+item[2]<400: #check if the salient regions are close from left side and right side of the ERP image
                        b = sqrt(((resolution[1]-max((item[0]+item[2]/2),(group_item[0]+group_item[2]/2))+min((item[0]+item[2]/2),(group_item[0]+group_item[2]/2)))**2) + (((item[1]+item[3]/2)-(group_item[1]+group_item[3]/2))**2))

                    if a <spatial_distance:
                        lower_distance = a
                        group = group_key
                        break


                    if b<spatial_distance:
                        lower_distance1 = b
                        group1 = group_key
                        break


                if lower_distance!=10000 or lower_distance1!=10000:

                    break


            if lower_distance<lower_distance1:

                if lower_distance!=10000 and lower_distance<spatial_distance:

                    groups_dict[group].append(item)
                    groups_frame[group].append(frame_number[i])

                    added_to_group = True
            if lower_distance1<lower_distance:

                if lower_distance1 != 10000 and lower_distance1 <spatial_distance:

                    groups_dict[group1].append(item)
                    groups_frame[group1].append(frame_number[i])

                    added_to_group = True
            if not added_to_group:
                #print(item[0])
                close = False
                for group_key in groups_dict.keys():
                    last_group_items = groups_dict[group_key][-5:]
                    last_frame = groups_frame[group_key][-5:]
                    current_frame = int(frame_number[i])

                    for l,group_item in enumerate(last_group_items):
                        if abs(sqrt((group_item[0]-item[0])**2+(group_item[1]-item[1])**2))<220 or abs(sqrt((resolution[1]-max(group_item[0],item[0])+min(group_item[0],item[0]))**2 + (group_item[1]-item[1])**2))<220 and abs(int(last_frame[l])-current_frame)<21:
                            #print("yes")
                            close=True
                            break
                if close==False:
                    #print("no close, so new item",item)
                    groups_dict[tuple(item)] = [item]
                    groups_frame[tuple(item)]= [frame_number[i]]

            #Hold together the salient regions, the frames, the scores a
            groups_dict = dict(sorted(groups_dict.items(), key=lambda item: len(item[1]), reverse=True))
            groups_frame = dict(sorted(groups_frame.items(), key=lambda item: len(item[1]), reverse=True))


    # Convert the dictionary values to the final lists
    result_lists = list(groups_dict.values())
    results_frames = list(groups_frame.values())


    list_of_2d_volumes,list_of_2d_frames = fill_loss_frames(result_lists,results_frames,fill_loss)


    return list_of_2d_volumes,list_of_2d_frames


def fill_loss_frames(lists_of_results1,lists_frames1,num_frames):
    lists_of_results = []
    lists_frames = []

    lists_of_results1 = [sublist for sublist in lists_of_results1 if len(sublist) >= 40]
    lists_frames1= [sublist for sublist in lists_frames1 if len(sublist) >= 40]

    for i in range(len(lists_of_results1)):
        new_frames = []
        new_results = []

        flag = False
        for j in range(len(lists_frames1[i]) - 1):
            dif = int(lists_frames1[i][j + 1]) - int(lists_frames1[i][j])

            new_frames.append(int(lists_frames1[i][j]))
            new_results.append(lists_of_results1[i][j])

            if (dif) <= num_frames:

                for r in range(dif - 1):
                    x = int(lists_frames1[i][j]) + r + 1
                    new_frames.append(x)
                    new_results.append(lists_of_results1[i][j])

            else:
                lists_of_results.append(new_results)
                lists_frames.append(new_frames)

                new_frames = []
                new_results = []


        lists_of_results.append(new_results)
        lists_frames.append(new_frames)
    final_salient_regions = [sublist for sublist in lists_of_results if len(sublist) >= 60]
    final_frames = [sublist for sublist in lists_frames if len(sublist) >= 60]

    combined = list(zip(final_frames, final_salient_regions))
    combined.sort(key=lambda x: x[0])

    # separate back into two lists
    final_frames, final_salient_regions= zip(*combined)


    print("Total subshots",len(final_frames))
    return final_salient_regions,final_frames