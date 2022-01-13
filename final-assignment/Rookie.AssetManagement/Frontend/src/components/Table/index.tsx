import React from "react";
import { CaretDownFill, CaretUpFill } from "react-bootstrap-icons";
import { DESCENDING } from "src/constants/paging";
import IColumnOption from "src/interfaces/IColumnOption";

import Paging, { PageType } from "./Paging";

export type SortType = {
  columnValue: string;
  orderBy: string;
};

type ColumnHeaderType = {
  colName: string;
  colValue: string;
  sortState: SortType;
  handleSort: (colValue: string) => void;
}

const ColumnHeader: React.FC<ColumnHeaderType> = ({ colName, colValue, sortState, handleSort }) => {
  return (
    <span style={(colValue == sortState.columnValue)
      ? { borderBottom: "3px solid grey", display: "block" }
      : {}}>
      <a className="btn" onClick={() => handleSort!(colValue)}>
        {colName}
        <ColumnIcon colValue={colValue} sortState={sortState} />
      </a>
    </span>
  );
};

type ColumnIconType = {
  colValue: string;
  sortState: SortType;
}

const ColumnIcon: React.FC<ColumnIconType> = ({ colValue, sortState }) => {
  if (colValue === sortState.columnValue && sortState.orderBy === DESCENDING)
    return (<CaretUpFill />);
  return (<CaretDownFill />);
};

type Props = {
  columns: IColumnOption[];
  children: React.ReactNode;
  sortState: SortType;
  handleSort: (colValue: string) => void;
  page?: PageType;
};

const Table: React.FC<Props> = ({ columns, children, page, sortState, handleSort }) => {
  // console.log(page)
  return (
    <>
      <div className="table-container">
        {(page && page.totalPages === 0)
          ? <p style={{ textAlign: "center" }}>No results</p>
          : (page && page.totalPages && page.totalPages > 0)
            ? <>
              <table className="table">
                <thead>
                  <tr>
                    {
                      columns.map((col, i) => (
                        <th key={i}>
                          <ColumnHeader
                            colName={col.columnName}
                            colValue={col.columnValue}
                            sortState={sortState}
                            handleSort={handleSort} />
                        </th>
                      ))
                    }
                  </tr>
                </thead>

                <tbody>
                  {children}
                </tbody>
              </table>
              {
                (page && page.totalPages && page.totalPages > 0) && <Paging {...page} />
              }
            </>
            : <p style={{ textAlign: "center" }}>Loading...</p>
        }
      </div>
    </>
  );
};

export default Table;
