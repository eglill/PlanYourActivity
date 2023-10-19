import React from 'react';

export const DescriptionListElem = ({description, value}: {description: string, value: string | number | boolean}) => {
    return (
        <>
            <dt className="col-sm-3">
                {description}
            </dt>
            <dd className="col-sm-9">
                {value}
            </dd>
        </>
    );
};